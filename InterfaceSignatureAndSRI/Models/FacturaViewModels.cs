using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace InterfaceSignatureAndSRI.Models
{
    public class FacturaViewModels
    {
        private string _stringConnection;
        private static string _NameDatabase = Properties.Settings.Default.NameDataBse;
        internal DateTime? fechaAutoriza;
        public DateTime fechaEmision;
        internal byte[] logoByte = null;
        internal string numeroAutorizacion;
        internal List<CADsisVenta.IMPUESTO_VALOR> listImpuestoValor;
        internal List<CADsisVenta.IMPUESTO> listImpuesto;
        internal string ClaveAcceso;
        internal string ambiente;
        internal string codDoc;
        public string Estado;
        public string InnerXml;
        public string Data;
        internal string RazonSocialComprador;
        internal string FacturaNum;
        public int IDRelationData;
        public string rucComprador;
        internal string CompanyName;
        internal string Phone;
        internal string CellPhone;

        public FacturaViewModels(string stringConnection)
        {
            _stringConnection = stringConnection;
        }

        public FacturaViewModels()
        {

        }

        public void GetInformation(int voucherID, bool isDataRelation = false)
        {
            string sql;
            Data = string.Empty;
            DateTime dateAuth;
            try
            {
                using (SqlConnection cnn = new SqlConnection(_stringConnection))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("", cnn))
                    {
                        sql = "Select * from [" + _NameDatabase + "].[dbo].[Voucher]" + "\n";
                        sql = sql + "WHERE VoucherID=" + voucherID;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = sql;

                        using (DataTable dt = new DataTable())
                        {
                            using (SqlDataAdapter dat = new SqlDataAdapter(cmd))
                            {
                                dat.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    DateTime.TryParse(dt.Rows[0]["fechaEmision"].ToString(), out fechaEmision);
                                    if (DateTime.TryParse(dt.Rows[0]["FechaAutorizacion"].ToString(), out dateAuth))
                                        this.fechaAutoriza = dateAuth;

                                    this.Data = dt.Rows[0]["Comprobante"].ToString();
                                    this.ClaveAcceso = dt.Rows[0]["ClaveAcceso"].ToString();
                                }
                            }

                            // get byte logo 
                            sql = "Select * from [dbo].[MySetting]";
                            cmd.CommandText = sql;

                            using (SqlDataAdapter dat = new SqlDataAdapter(cmd))
                            {
                                dt.Clear();
                                dat.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    this.logoByte = (byte[])dt.Rows[0]["ImageLogo"];
                                    this.CompanyName = dt.Rows[0].Field<string>("CompanyName");
                                    this.Phone = dt.Rows[0].Field<string>("Phone");
                                    this.CellPhone = dt.Rows[0].Field<string>("CellPhone");
                                }

                            }

                        }
                    }
                }

                MappingInfor(this.Data);
            }
            catch (Exception ex)
            {

                Microsoft.VisualBasic.Interaction
                    .MsgBox(ex.Message + "\n" + ex.StackTrace);
            }
        }
        public void GetInformationVat()
        {
            string sql;
            try
            { // get list vat
                sql = "Select * from sri.IMPUESTO";
                this.listImpuesto = CADsisVenta.Funtions.Funtion.GetListIMPUESTO();

                // get list vat valor
                sql = "select  * from sri.IMPUESTO_VALOR where CODIGO_IMPUESTO  = 2 and TIPO_IMPUESTO = 'I'";
                this.listImpuestoValor = CADsisVenta.Funtions.Funtion.GetListIVA();
                //get logo 
                if (this.logoByte == null || this.logoByte.Length == 0)
                    this.logoByte = CADsisVenta.Funtions.Funtion.GetLogoPDFByte();

            }
            catch (Exception ex)
            {
                Microsoft.VisualBasic.Interaction
                    .MsgBox(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void SetInformatio(string ReadXml, DateTime? fechaEmision, byte[] imageLogo = null)
        {
            MappingInfor(ReadXml);
            if (this.Data == null)
                this.Data = ReadXml;

            if (fechaEmision.HasValue)
            {
                this.fechaEmision = (DateTime)fechaEmision.Value;
            }
            if (imageLogo != null)
            {
                this.logoByte = imageLogo;
            }

        }

        private void MappingInfor(string data)
        {
            try
            {
                this.Data = data;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Data);

                XmlNode nodeComprobante;

                var nodeEstado = doc.GetElementsByTagName("estado")[0];
                if (nodeEstado != null)
                    this.Estado = nodeEstado.InnerText;
                else
                {
                    nodeComprobante = doc.SelectSingleNode("//*[@id='comprobante']");
                    if (nodeComprobante == null)
                        goto ExitBlock;

                }


                var isnum = doc.GetElementsByTagName("numeroAutorizacion")[0];
                if (isnum != null)
                {
                    numeroAutorizacion = doc.GetElementsByTagName("numeroAutorizacion")[0].InnerText;
                }
                var fechaAutoriz = doc.GetElementsByTagName("fechaAutorizacion")[0];
                if (fechaAutoriz != null)
                {
                    DateTime date;
                    if (DateTime.TryParse(doc.GetElementsByTagName("fechaAutorizacion")[0].InnerText, out date))
                        this.fechaAutoriza = date;
                }
                else
                {
                    this.fechaAutoriza = null;
                }

                var comprobante = doc.GetElementsByTagName("comprobante")[0];
                if (comprobante != null)
                {
                    string docXml = doc.GetElementsByTagName("comprobante")[0].InnerText;
                    docXml = docXml.Replace("<![CDATA[", "");
                    docXml = docXml.Replace("]]>", "");

                    doc.LoadXml(docXml);
                }

                nodeComprobante = doc.SelectSingleNode("//*[@id='comprobante']");
                if (nodeComprobante != null)
                    goto readXmlDocument;
                else
                    goto ExitBlock;

                readXmlDocument:
                {
                    doc.LoadXml(nodeComprobante.OwnerDocument.InnerXml);

                    var claveNode = doc.GetElementsByTagName("claveAcceso");
                    if (claveNode != null && claveNode.Count > 0)
                        ClaveAcceso = doc.GetElementsByTagName("claveAcceso")[0].InnerText;

                    ambiente = doc.GetElementsByTagName("ambiente")[0].InnerText;
                    codDoc = doc.GetElementsByTagName("codDoc")[0].InnerText;
                    if (codDoc.Equals("01"))
                    {
                        var fechaEmision = doc.GetElementsByTagName("fechaEmision")[0];
                        if (fechaEmision != null)
                        {
                            DateTime date;
                            if (DateTime.TryParse(doc.GetElementsByTagName("fechaEmision")[0].InnerText, out date))
                                this.fechaEmision = date;
                        }


                        RazonSocialComprador = doc.GetElementsByTagName("razonSocialComprador")[0].InnerText;
                        rucComprador = doc.GetElementsByTagName("identificacionComprador")[0].InnerText;
                    }
                    else if (codDoc.Equals("02"))
                    {
                        RazonSocialComprador = doc.GetElementsByTagName("razonSocialSujetoRetenido")[0].InnerText;
                    }
                    else if (codDoc.Equals("06"))
                    {
                        RazonSocialComprador = doc.GetElementsByTagName("razonSocialTransportista")[0].InnerText;
                    }
                    else if (codDoc.Equals("07"))
                    {
                        RazonSocialComprador = doc.GetElementsByTagName("razonSocialSujetoRetenido")[0].InnerText;
                    }
                    else
                    {
                        RazonSocialComprador = "No pudo leer  codDoc:" + codDoc;
                    }

                    if (!codDoc.Equals("01"))
                    {
                        Interaction.MsgBox("No es FACTURA es posible que no tenga suerte para extraer..", MsgBoxStyle.Exclamation, "Import..");
                    }


                    FacturaNum = doc.GetElementsByTagName("estab")[0].InnerText + "-" +
                        doc.GetElementsByTagName("ptoEmi")[0].InnerText + "-" +
                        doc.GetElementsByTagName("secuencial")[0].InnerText;
                }

            ExitBlock:
                XmlElement Root = doc.DocumentElement;
                XmlNode NodoEliminar = Root.GetElementsByTagName("ds:Signature")[0];
                if (NodoEliminar != null)
                {
                    Root.RemoveChild(NodoEliminar);
                }
                this.InnerXml = doc.InnerXml;
            }
            catch (Exception ex)
            {

                Interaction.MsgBox(ex.Message + "\n" +
                    ex.StackTrace, MsgBoxStyle.Critical, "Error");
            }

        }
        public string GetAmbiente()
        {
            return ambiente;

        }

    }

}
