using Domain.Data;
using Domain.Data.Entities;
using Domain.Data.Enums;
using Domain.Models;
using ec.gob.sri.Xml.modelo_v1_1_0.Factura;
using java.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.GeneratedXML
{
    public static class FacturaXmlBuilder
    {

        public static async Task<ItemCommerce> GetCommerceAsync(DomainDataContext db, int wareHouseId)
        {

            var myCommerce =  db.MyCommerce.Select(op => new ItemCommerce
            {
                Id = op.CommerceId,
                AdressCompany = op.Domicilio,
                CellPhone = op.Phone,
                Phone = op.Phone,
                Ruc = op.Ruc,
                NameComercial = op.NameComercial,
                RazonSocial = op.RazonSocial,
                NameCompay = op.Company,
                AgenteRetencion = op.AgenteRetencion,
                IdTypeRegimen = (TypeECommerceEnum)op.IdTypeRegimen,
                SpecialTaxNumber = op.SpecialTaxNumber,
                KeepAccounting = op.KeepAccounting,
                Moneda = op.TypoMonedaDecrip,
                RegimenMicroempresas = op.RegimenMicroempresas,
                ContribuyenteRimpe = op.ContribuyenteRimpe,
                TIPO_EMISION  = op.SignatureOptions.FirstOrDefault().TIPO_EMISION
            }).FirstOrDefault();

            db.Bodegas.Where(op => op.IdBodega == wareHouseId).ToList().ForEach(op =>
            {
                myCommerce.EstablishmentAddress = op.DireccionBodega;
                myCommerce.CodEstablecimiento = op.CodEstablec;
            });


            return await Task.FromResult(myCommerce);   
        }


        public  static  async Task<List<facturaDetalle>> GetFacturaDetalles( List<FacturaVentaDetail_x> _listSales, List<ItemsImpuestValor> itemsImpuests)
        {
            return await Task.Factory.StartNew(() =>
            {

                List<facturaDetalle> lisDetall = new List<facturaDetalle>();


                foreach (var item in _listSales)
                {
                    BigDecimal bigDecimal = new BigDecimal(1.000000);
                    decimal result;
                    Decimal.TryParse("0.000000", out result);
                    var pvp = ((item.Prec_Venta - item.Iva) + item.Discount) / item.Cantidad;
                    var sTotal = item.Prec_Venta - item.Iva;

                    facturaDetalle facDetail = new facturaDetalle
                    {

                        cantidad = (double)item.Cantidad,
                        codigoPrincipal = item.PresentID.ToString(),
                        codigoAuxiliar = item.ProductId.ToString(),
                        descripcion = item.ProductName,
                        descuento = item.Discount,
                        precioUnitario = Math.Round(pvp, 5),
                        precioTotalSinImpuesto = Math.Round(sTotal, 2),
                        unidadMedida = "UDN",
                    };

                    List<impuesto> lisImp = new List<impuesto>();



                    foreach (var vat in itemsImpuests.Where(x => x.PresentId == item.PresentID))
                    {

                        lisImp.Add(new impuesto
                        {
                            codigo = vat.CODIGO,
                            codigoPorcentaje = vat.CODIGO_TIPO_IMPUESTO,
                            baseImponible = vat.BaseImponible,
                            tarifa = vat.PORCENTAJE,
                            valor = Math.Round((vat.Valor), 2),
                        });
                    }

                    facDetail.impuestos = lisImp.ToArray();

                    // no se agrego info adicional en este diseño

                    lisDetall.Add(facDetail);

                }

                return lisDetall;
            });
        }



        public async static Task<facturaCampoAdicional[]> GetInfoAdicional(Cliente _customer, DateTime _fechaEmision, int numInterno)
        {

            return await Task<facturaCampoAdicional[]>.Factory.StartNew(() =>
            {
                List<facturaCampoAdicional> infAdi = new List<facturaCampoAdicional>();


                infAdi.Add(new facturaCampoAdicional
                {
                    nombre = "Hora",
                    Value = _fechaEmision.ToString("H:mm:ss")
                });

                infAdi.Add(new facturaCampoAdicional
                {
                    nombre = "RUC Proveedor",
                    Value = $"0602832255001"
                });


                infAdi.Add(new facturaCampoAdicional
                {
                    nombre = "Cod.Cliente",
                    Value = _customer.Personas.IdPersona.ToString()
                });

                infAdi.Add(new facturaCampoAdicional
                {
                    nombre = "Dirección",
                    Value = string.IsNullOrEmpty(_customer.Personas.Direccion) ?
                          "-" : _customer.Personas.Direccion
                });


                infAdi.Add(new facturaCampoAdicional
                {
                    nombre = "Teléfono",
                    Value = string.IsNullOrEmpty(_customer.Personas.Telefono) ?
                            "-" : _customer.Personas.Telefono
                });

                infAdi.Add(new facturaCampoAdicional
                {
                    nombre = "Nro Interno",
                    Value = numInterno.ToString()
                });
                return infAdi.ToArray();
            });

        }

    }
}
