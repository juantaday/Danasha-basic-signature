using CADsisVenta;
using Domain.Helpers;
using ec.gob.sri.comprobantes.Enum;
using ec.gob.sri.Xml;
using ec.gob.sri.Xml.modelo_v1_1_0.Factura;
using InterfaceSignatureAndSRI.Helpers;
using InterfaceSignatureAndSRI.Models;
using InterfaceSignatureAndSRI.Utils;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace InterfaceSignatureAndSRI.Data
{
    public static class CRUD_database
    {

        private static DataClassesDBDataContext dbReflexion = new DataClassesDBDataContext(DomainSQLite.Setting.Configuration.ConectionString);
        private static string sql;
        private static string _NameDatabase = Properties.Settings.Default.NameDataBse;


        internal static bool DeleteSingned(int voucherID)
        {
            var stringConnection = dbReflexion.Connection.ConnectionString;
            using (SqlConnection cnn = new SqlConnection(stringConnection))
            {
                cnn.Open();

                using (SqlCommand cmd = new SqlCommand("", cnn))
                {
                    sql = "DELETE [" + _NameDatabase + "].[dbo].[Voucher]" + "\n";
                    sql = sql + "WHERE VoucherID=" + voucherID;

                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static int InsertGenerated(string claveAcceso, System.Xml.XmlDocument doc, int idFactura,
       string codDoc, DateTime _fechaEmision, string estado = "")
        {
            var stringConnection = dbReflexion.Connection.ConnectionString;
            DateTime dateAutorization;

            string sql;
            string xmlText = doc.InnerXml;

            int luk = xmlText.LastIndexOf('?');
            if (luk > 5)
            {
                xmlText = xmlText.Remove(0, luk + 2);
            }

            using (SqlConnection cnn = new SqlConnection(stringConnection))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand("", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    string field = @"(
                             IDRelationData,
                             TypesVoucherID,
                             ClaveAcceso,
                             Estado,
                             FechaAutorizacion,
                             FechaEmision,
                             Comprobante)";

                    sql = "INSERT [" + _NameDatabase + "].[dbo].[Voucher]";
                    sql = sql + "\n" + field;
                    sql = sql + "\n Values(" + idFactura + "," +
                        "(Select top(1) TypesVoucherID \n From [" + _NameDatabase + "].[dbo].[TypesVoucher] " +
                        "\n" + "Where Code =@codDoc), @ClaveAcceso, " +
                        "@estado, @dateAutorization, @FechaEmision, @xmlText)" + "\n";
                    sql = sql + @"Set @VoucherID = SCOPE_IDENTITY();";

                    cmd.CommandText = sql;

                    cmd.Parameters.Add("@FechaEmision", sqlDbType: System.Data.SqlDbType.DateTime);
                    cmd.Parameters.Add("@dateAutorization", sqlDbType: System.Data.SqlDbType.DateTime);
                    cmd.Parameters.Add("@ClaveAcceso", sqlDbType: System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@estado", sqlDbType: System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@codDoc", sqlDbType: System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@xmlText", sqlDbType: System.Data.SqlDbType.VarChar);

                    if (DateTime.TryParse(doc.GetElementsByTagName("claveAcceso")[0].InnerText, out dateAutorization))
                    {
                        cmd.Parameters["@dateAutorization"].Value = dateAutorization;
                    }
                    else
                    {
                        cmd.Parameters["@dateAutorization"].Value = DBNull.Value;
                    }
                    cmd.Parameters["@FechaEmision"].Value = _fechaEmision;
                    cmd.Parameters["@ClaveAcceso"].Value = claveAcceso;
                    if (string.IsNullOrEmpty(estado))
                    {
                        cmd.Parameters["@estado"].Value = "Generado";
                    }
                    else
                    {
                        cmd.Parameters["@estado"].Value = estado;
                    }

                    cmd.Parameters["@xmlText"].Value = xmlText;
                    cmd.Parameters["@codDoc"].Value = codDoc;

                    cmd.Parameters.Add("@VoucherID", SqlDbType.Int);
                    cmd.Parameters["@VoucherID"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    return (int)cmd.Parameters["@VoucherID"].Value;

                }

            }

        }

        public static DataSet GetDataSetWitID(int voucherID)
        {

            var stringConnection = dbReflexion.Connection.ConnectionString;
            try
            {
                FacturaViewModels viewmodel =
                new FacturaViewModels(stringConnection);
                viewmodel.GetInformation(voucherID);
                return GetDataSetWithModel(viewmodel);
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message + "\n" +
                    ex.StackTrace, MsgBoxStyle.Critical, "Error");
                return new DataSet();
            }

        }


        public static DataSet GetDataSetWithModel(FacturaViewModels viewmodel)
        {
            DataSet ds = new DataSet();
            try
            {

                if (viewmodel.InnerXml.Length > 0)
                {
                    Dictionary<int, string> infoAdiotional = new Dictionary<int, string>();

                    if (viewmodel.listImpuesto == null || viewmodel.listImpuestoValor == null)
                    {
                        viewmodel.GetInformationVat();
                    }

                    factura fac = XMLSerializers.Deserialize<factura>(viewmodel.InnerXml);

                    fac.Signature = null;
                    int _key = 0;
                    string infoToString = "";

                    foreach (var item in fac.detalles)
                    {
                        if (item.detallesAdicionales == null)
                            infoAdiotional.Add(_key, "");
                        else
                        {
                            infoToString = string.Empty;
                            for (int i = 0; i < item.detallesAdicionales.Length; i++)
                            {
                                facturaDetalleDetAdicional inf = item.detallesAdicionales[i];
                                if (!string.IsNullOrEmpty(infoToString))
                                    infoToString += "; ";

                                infoToString += inf.nombre + ": " + inf.valor;
                            }
                            infoAdiotional.Add(_key, infoToString);
                        }

                        item.detallesAdicionales = null;
                        _key++;
                    }

                    string xml = XMLSerializers.Serialize(fac);

                    ds.InferXmlSchema(new StringReader(xml), null);

                    MappingFactura.ConfigColumnType(ds);
                    ds.ReadXml(new StringReader(xml), XmlReadMode.IgnoreSchema);

                    //seleciono la tabla y luego añado los campos
                    {
                        DataTable infoTribu = ds.Tables["infoTributaria"];
                        infoTribu.Columns.Add(new DataColumn("fechaAutorizacion", Type.GetType("System.String")));
                        infoTribu.Columns.Add(new DataColumn("fechaEmision", Type.GetType("System.String")));
                        infoTribu.Columns.Add(new DataColumn("autorizacion", Type.GetType("System.String")));
                        infoTribu.Columns.Add(new DataColumn("logoByte", Type.GetType("System.Byte[]")));
                        infoTribu.Columns.Add(new DataColumn("codBarrClaveAcceso", Type.GetType("System.Byte[]")));


                        if (!infoTribu.ContainColumn("contribuyenteRimpe"))
                        {
                            infoTribu.Columns.Add(new DataColumn("contribuyenteRimpe", Type.GetType("System.String")));
                        }

                        if (!infoTribu.ContainColumn("agenteRetencion"))
                        {
                            infoTribu.Columns.Add(new DataColumn("agenteRetencion", Type.GetType("System.String")));
                        }

                        if (!infoTribu.ContainColumn("regimenMicroempresas"))
                        {
                            infoTribu.Columns.Add(new DataColumn("regimenMicroempresas", Type.GetType("System.String")));
                        }

                        if (!infoTribu.ContainColumn("nombreComercial"))
                        {
                            infoTribu.Columns.Add(new DataColumn("nombreComercial", Type.GetType("System.String")));
                        }


                        foreach (DataRow rows in infoTribu.Rows)
                        {
                            rows["autorizacion"] = viewmodel.numeroAutorizacion;
                            if (viewmodel.fechaAutoriza.HasValue)
                            {
                                rows["fechaAutorizacion"] = viewmodel.fechaAutoriza.Value.ToString("dd-MM-yyyy ' T ' HH:mm:ss");
                            }
                            if (viewmodel.fechaEmision.Year > 1)
                            {
                                rows["fechaEmision"] = viewmodel.fechaEmision.ToString("dd-MM-yyyy ' T ' HH:mm:ss");
                            }

                            rows["codBarrClaveAcceso"] = ImageHelper.ImageToByteArray(BarCodeClass.codigo128("A" + fac.infoTributaria.claveAcceso + "B", false, 40));

                            //tipoEmision 
                            if (rows["tipoEmision"].ToString().Equals("1"))
                                rows["tipoEmision"] = "NORMAL";
                            else
                                rows["tipoEmision"] = "DESCONOCIDO";
                            //Ambiente
                     
                            rows["ambiente"] =  ec.gob.sri.comprobantes.Enum.TipoAmbienteEnum.GetNameWithCodeString(rows["ambiente"].ToString());

                            rows["logoByte"] = viewmodel.logoByte;

                        }
                        infoTribu.EndLoadData();
                    }
                    //seelct info factura
                    {
                        DataTable infoFactura = ds.Tables["infoFactura"];
                        if (!infoFactura.ContainColumn("contribuyenteEspecial"))
                        {
                            infoFactura.Columns.Add(new DataColumn("contribuyenteEspecial", Type.GetType("System.String")));
                        }

                    }

                    //para agregar iva
                    {
                        var detalles = fac.detalles;
                        _key = 0;
                        foreach (DataRow rows in ds.Tables["detalle"].Rows)
                        {
                            //descripcion
                            var infoAdd = infoAdiotional.Where(x => x.Key == _key).FirstOrDefault();
                            if (!string.IsNullOrEmpty(infoAdd.Value))
                                rows["descripcion"] = rows["descripcion"].ToString() + "\n" + infoAdd.Value;

                            string idDetal = rows["codigoPrincipal"].ToString();

                            var iva = detalles.
                                Where(x => x.codigoPrincipal == idDetal)
                                .FirstOrDefault().impuestos.FirstOrDefault()
                                .valor;
                            rows["iva"] = iva;

                            _key++;
                        }
                    }
                    //modificar forma de pago
                    {
                        var dbPago = ds.Tables["pago"];
                        foreach (DataRow rows in dbPago.Rows)
                        {
                            rows["formaPago"] = EnumTypePago.valueOf((string)rows["formaPago"]).getName();
                        }
                    }
                    //carga totales
                    {

                        DataTable totals = ds.Tables["totalImpuesto"];

                        DataTable totalDetaImpuesto = ds.Tables.Add("totalDetaImpuesto");

                        totalDetaImpuesto.Columns.Add(new DataColumn("index", Type.GetType("System.Int16")));
                        totalDetaImpuesto.Columns.Add(new DataColumn("codigo", Type.GetType("System.String")));
                        totalDetaImpuesto.Columns.Add(new DataColumn("descripcionImpuesto", Type.GetType("System.String")));
                        totalDetaImpuesto.Columns.Add(new DataColumn("codigoPorcentaje", Type.GetType("System.String")));
                        totalDetaImpuesto.Columns.Add(new DataColumn("baseImponible", Type.GetType("System.Decimal")));
                        totalDetaImpuesto.Columns.Add(new DataColumn("tarifa", Type.GetType("System.Decimal")));
                        totalDetaImpuesto.Columns.Add(new DataColumn("valor", Type.GetType("System.Decimal")));

                        //base de impuestos de iva

                        foreach (IMPUESTO_VALOR item in viewmodel.listImpuestoValor.Where(x => x.CODIGO_IMPUESTO == 2))
                        {
                            var newRow = totalDetaImpuesto.NewRow();

                            var listIva = totals.Select($"codigoPorcentaje = '{item.CODIGO}'").FirstOrDefault();

                            newRow["index"] = item.CODIGO;
                            newRow["codigo"] = item.CODIGO_IMPUESTO;
                            newRow["descripcionImpuesto"] = $"SUBTOTAL {item.DESCRIPCION}";
                            newRow["codigoPorcentaje"] = item.PORCENTAJE;
                            newRow["baseImponible"] = listIva == null ? 0.00M : listIva.Field<decimal>("baseImponible");
                            newRow["valor"] = listIva == null ? 0.00M : listIva.Field<decimal>("baseImponible");

                            totalDetaImpuesto.Rows.Add(newRow);

                        }

                        DataTable infoFactur = ds.Tables["infoFactura"];
                        //sub totales sin impuesto total sescuento ..
                        {
                            var newRow = totalDetaImpuesto.NewRow();
                            newRow["index"] = ("11");
                            newRow["descripcionImpuesto"] = "SUBTOTAL SIN IMPUESTOS";
                            newRow["valor"] = infoFactur.Rows[0].Field<decimal>("totalSinImpuestos");
                            totalDetaImpuesto.Rows.Add(newRow);

                            newRow = totalDetaImpuesto.NewRow();
                            newRow["index"] = ("12");
                            newRow["descripcionImpuesto"] = "DESCUENTO";
                            newRow["valor"] = infoFactur.Rows[0].Field<decimal>("totalDescuento");
                            totalDetaImpuesto.Rows.Add(newRow);

                            newRow = totalDetaImpuesto.NewRow();
                            newRow["index"] = ("13");
                            newRow["descripcionImpuesto"] = "PROPINA";
                            newRow["valor"] = infoFactur.Rows[0].Field<decimal>("propina");
                            totalDetaImpuesto.Rows.Add(newRow);

                        }


                        //ingreso valor de iva 

                        foreach (var item in totals.Select("valor > 0  AND codigo=2"))
                        {
                            var newRow = totalDetaImpuesto.NewRow();

                            var listIva = viewmodel.listImpuestoValor
                                .Where(x => x.CODIGO == item.Field<string>("codigoPorcentaje")).FirstOrDefault();

                            newRow["index"] = ("2" + item.Field<Int32>("codigo").ToString());
                            newRow["codigo"] = item.Field<Int32>("codigo").ToString();
                            newRow["descripcionImpuesto"] = $"IVA {listIva.DESCRIPCION}";
                            newRow["codigoPorcentaje"] = item.Field<string>("codigoPorcentaje");
                            newRow["baseImponible"] = item.Field<decimal>("baseImponible");
                            newRow["valor"] = item.Field<decimal>("valor");

                            totalDetaImpuesto.Rows.Add(newRow);

                        }

                        //ingreso valor de Ice IRBPNR 
                        foreach (var item in viewmodel.listImpuesto.Where(x => x.CODIGO > 2))
                        {
                            var newRow = totalDetaImpuesto.NewRow();

                            DataRow[] listIva = totals.Select($"codigo={item.CODIGO}");


                            newRow["index"] = ("3" + item.CODIGO.ToString());
                            newRow["codigo"] = item.CODIGO;
                            newRow["descripcionImpuesto"] = item.TAIM_DES_IMP;
                            newRow["codigoPorcentaje"] = listIva.Length == 0 ? "" : listIva.FirstOrDefault().Field<string>("codigoPorcentaje");
                            newRow["baseImponible"] = listIva.Length == 0 ? 0.00M : listIva.Sum(x => x.Field<decimal>("baseImponible"));
                            newRow["valor"] = listIva.Length == 0 ? 0.00M : listIva.Sum(x => x.Field<decimal>("valor"));

                            totalDetaImpuesto.Rows.Add(newRow);

                        }
                        //total
                        {

                            var newRow = totalDetaImpuesto.NewRow();
                            newRow["index"] = ("999");
                            newRow["descripcionImpuesto"] = "VALOR TOTAL";
                            newRow["valor"] = infoFactur.Rows[0].Field<decimal>("importeTotal");
                            totalDetaImpuesto.Rows.Add(newRow);
                        }


                    }
                    foreach (DataTable dataTable in ds.Tables)
                    {
                        dataTable.EndLoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message + "\n" +
                   ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }

            return ds;
        }

        /// <summary>
        /// Guarda en base de datos los xml autorizados o no autorizados
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="xml">xml sin <?sql ></param>
        /// <param name="getEstado"></param>
        /// <param name="fechaAutorize"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        internal static int UpdateAutorize(int voucherID, string xml, string getEstado, DateTime fechaAutorize, string errorMessage)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return -1;
            }

            int luk = xml.IndexOf("?><");
            if (luk > 5)
            {
                xml = xml.Remove(0, luk + 2);
            }

            string sql;
            var stringConnection = dbReflexion.Connection.ConnectionString;
            using (SqlConnection cnn = new SqlConnection(stringConnection))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand("", cnn))
                {
                    sql = "UPDATE [" + _NameDatabase + "].[dbo].[Voucher]" + "\n";
                    sql = sql + @"SET Estado= @estado
                        ,Comprobante=@Comprobante
                        ,FechaAutorizacion=@DateAutorize
                        ,ErrorMesage =@ErrorMesage" + "\n";
                    sql = sql + "WHERE VoucherID=" + voucherID;
                    cmd.CommandType = System.Data.CommandType.Text;
                    //add the parameter
                    cmd.Parameters.Add("@Comprobante", SqlDbType.VarChar);
                    cmd.Parameters.Add("@DateAutorize", SqlDbType.DateTime2);
                    cmd.Parameters.Add("@ErrorMesage", SqlDbType.VarChar);
                    cmd.Parameters.Add("@estado", SqlDbType.VarChar);
                    // set value parameters
                    cmd.Parameters["@Comprobante"].Value = xml;
                    cmd.Parameters["@DateAutorize"].Value = fechaAutorize;
                    cmd.Parameters["@estado"].Value = getEstado;
                    if (!getEstado.Equals("AUTORIZADO"))
                    {
                        cmd.Parameters["@ErrorMesage"].Value = errorMessage;
                    }
                    else
                    {
                        cmd.Parameters["@ErrorMesage"].Value = DBNull.Value;
                    }

                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    return voucherID;
                }
            }

        }

        internal static void UpdateEnter(int voucherID)
        {
            var stringConnection = dbReflexion.Connection.ConnectionString;
            using (SqlConnection cnn = new SqlConnection(stringConnection))
            {
                cnn.Open();

                using (SqlCommand cmd = new SqlCommand("", cnn))
                {
                    sql = "UPDATE [" + _NameDatabase + "].[dbo].[Voucher]" + "\n";
                    sql = sql + "SET Estado=@Estado" + "\n";
                    sql = sql + "WHERE VoucherID=" + voucherID;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.Add("@Estado", SqlDbType.VarChar);
                    cmd.Parameters["@Estado"].Value = "Enviado";

                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void UpdateRejected(int voucherID, string mesage)
        {
            var stringConnection = dbReflexion.Connection.ConnectionString;
            using (SqlConnection cnn = new SqlConnection(stringConnection))
            {
                cnn.Open();

                using (SqlCommand cmd = new SqlCommand("", cnn))
                {
                    sql = "UPDATE [" + _NameDatabase + "].[dbo].[Voucher] ";
                    sql = sql + "SET Estado=@estado, ErrorMesage=@error \n";
                    sql = sql + "WHERE VoucherID=" + voucherID;

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("@estado", SqlDbType.VarChar);
                    cmd.Parameters.Add("@error", SqlDbType.VarChar);

                    cmd.Parameters["@estado"].Value = "DEVUELTA";
                    cmd.Parameters["@error"].Value = mesage;


                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static bool UpdateSingned(int voucherID, string xmlFirmado)
        {
            var stringConnection = dbReflexion.Connection.ConnectionString;
            using (SqlConnection cnn = new SqlConnection(stringConnection))
            {
                cnn.Open();

                int luk = xmlFirmado.LastIndexOf('?');
                if (luk > 5)
                {
                    xmlFirmado = xmlFirmado.Remove(0, luk + 2);
                }
                using (SqlCommand cmd = new SqlCommand("", cnn))
                {
                    sql = "UPDATE [" + _NameDatabase + "].[dbo].[Voucher]" + "\n";
                    sql = sql + @"SET  Comprobante = @Comprobante,
                         Estado=@Estado, ErrorMesage =@ErrorMesage " + "\n";
                    sql = sql + "WHERE VoucherID=" + voucherID;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.Add("@Comprobante", SqlDbType.VarChar);
                    cmd.Parameters.Add("@Estado", SqlDbType.VarChar);
                    cmd.Parameters.Add("@ErrorMesage", SqlDbType.VarChar);

                    cmd.Parameters["@Comprobante"].Value = xmlFirmado;
                    cmd.Parameters["@Estado"].Value = "Firmado";
                    cmd.Parameters["@ErrorMesage"].Value = DBNull.Value;

                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery() != 0;
                }
            }
        }


    }
}

