using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.Data
{
    public static class MappingFactura
    {
        public static void ConfigColumnType(DataSet ds)
        {
            try
            {
                string[] fielFactura = new string[]
                {
                    "cantidad",
                    "precioUnitario",
                    "descuento" ,
                    "precioTotalSinImpuesto"
                };
                var detalle = ds.Tables["detalle"];
                foreach (var item in fielFactura)
                {
                    detalle.Columns[item].DataType = typeof(Decimal);
                }
                detalle.Columns.Add(new DataColumn("iva", Type.GetType("System.Decimal")));

                var unidadMedida = detalle.Columns["unidadMedida"];
                if (unidadMedida == null)
                {
                    detalle.Columns.Add(new DataColumn("unidadMedida", Type.GetType("System.String")));
                }

                string[] fielInforFac = new string[]
                {
                    "totalSinImpuestos",
                    "totalDescuento" ,
                    "propina",
                    "importeTotal","importeTotal","totalDescuento","propina"
                };
                var inforFac = ds.Tables["infoFactura"];
                foreach (var item in fielInforFac)
                {
                    inforFac.Columns[item].DataType = typeof(Decimal);
                }

                var detAdicional = ds.Tables["detAdicional"];
                if (detAdicional == null)
                {
                    ds.Tables.Add("detAdicional");

                    ds.Tables["detAdicional"].Columns.Add(
                        new DataColumn("valor",
                        Type.GetType("System.String")));
                    ds.Tables["detAdicional"].Columns.Add(
                       new DataColumn("nombre",
                       Type.GetType("System.String")));
                }

                DataTable totalImpuesto = ds.Tables["totalImpuesto"];
                foreach (DataColumn item in totalImpuesto.Columns)
                {
                    if (item.ColumnName.Equals("codigo"))
                    {
                        item.DataType = Type.GetType("System.Int32");
                    }
                    else if (item.ColumnName.Equals("baseImponible"))
                    {
                        item.DataType = Type.GetType("System.Decimal");
                    }
                    else if (item.ColumnName.Equals("tarifa"))
                    {
                        item.DataType = Type.GetType("System.Decimal");
                    }
                    else if (item.ColumnName.Equals("valor"))
                    {
                        item.DataType = Type.GetType("System.Decimal");
                    }
                }


            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message + "\n" +
                    ex.StackTrace, MsgBoxStyle.Critical, "Error");

            }
        }
    }
}
