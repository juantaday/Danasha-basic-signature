using Domain.Data;
using Domain.Data.Entities;
using Domain.Data.Enums;
using Domain.Helpers;
using Domain.Models;
using ec.gob.sri.comprobantes.Enum;
using ec.gob.sri.Xml;
using ec.gob.sri.Xml.modelo_v1_1_0.Factura;
using InterfaceSignatureAndSRI.Utils;
using java.math;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterfaceSignatureAndSRI.GeneratedXML
{
    /// <summary>
    /// FacturaXMLWithList
    /// </summary>
    public class FacturaXMLWithList : IDisposable
    {
        private static string _ambiente;
        private static List<ItemSalesViewModel> _listSales;
        private static factura _fact;
        private static DateTime _fechaEmision;
        private static List<string> _emailSend;
        private static DomainDataContext db;
        private static InvoiceHeaderInput _ItemsVats;
        private static string claveAcceso;
        private static string Num_factura;
        private static string codDdoc;
        private static int _secuancia;
        private static ItemCommerce _myCommerce;
        private static Cliente _customer;
        private static FORMAS_PAGO _formas_pago;
        private static List<ItemVats> _listVats;

        /// <summary>
        /// Generador de xml 
        /// </summary>
        /// <param name="ambiente"></param>
        /// <param name="fechaEmision"></param>
        /// <param name="customer"></param>
        /// <param name="forma_pago"></param>
        /// <param name="listSales"></param>
        [Obsolete("Use el nuevo constructor de FacturaXMLWithId.")]
        public FacturaXMLWithList(string ambiente, DateTime fechaEmision, Cliente customer,
            FORMAS_PAGO forma_pago, List<ItemSalesViewModel> listSales)
        {
            _fact = new factura();
            _ambiente = ambiente;
            _listSales = listSales;
            _fechaEmision = fechaEmision;
            _secuancia = 0;
            _customer = customer;
            _formas_pago = forma_pago;
            _listVats = new List<ItemVats>();

            db = new DomainDataContext(new DbContextOptions<DataContext>());

        }

        public async Task<string> GetXmlFactura()
        {
            _ItemsVats = new InvoiceHeaderInput();
            _fact.version = "1.1.0";
            _fact.id = facturaID.comprobante;
            _fact.idSpecified = true;

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    TypeDocument numerator = null;

                    if (_ambiente.Equals("-1"))
                    {
                        numerator = db.TypeDocuments.Where(x => x.NameDocument.Equals("Nota de Venta")).FirstOrDefault();
                        _ambiente = "1";//cambio al ambiente de prueba....
                    }
                    else
                        numerator = db.TypeDocuments.Where(x => x.NameDocument.Equals("Factura")).FirstOrDefault();

                    if (numerator == null)
                        throw new Exception("No esta configurado la _secuancia se documentos");

                    _secuancia = numerator.Numeration;

                    _myCommerce = await FacturaXmlBuilder.GetCommerceAsync(db, _ItemsVats.WareHouseId);

                    _fact.infoTributaria = await getInfoTributaria();
                    _fact.infoFactura = await getInfoFactura();
                    _fact.detalles = await getFacturaDetalles();    
                    _fact.infoAdicional = await FacturaXmlBuilder.GetInfoAdicional(_customer, _fechaEmision, _secuancia);

                    var xml = XMLSerializers.Serialize(_fact, "");

                    _secuancia += 1;

                    numerator.Numeration = _secuancia;

                    db.Entry(numerator).State = EntityState.Modified;

                    await db.SaveChangesAsync();

                    transaccion.Commit();

                    return xml;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }

            }


        }

        private async static Task<infoTributaria> getInfoTributaria()
        {

            return await Task.Factory.StartNew(() =>
            {


                infoTributaria trib = new infoTributaria();

                _ItemsVats.Total = _listSales.Sum(x => x.Iva + x.ICE + x.IRBPNR);
                _ItemsVats.BaseIva = _listSales.Sum(x => x.BaseIva12);
                _ItemsVats.TotalWithOutVat = _listSales.Sum(x => x.SubTotal);
                _ItemsVats.IVA = _listSales.Sum(x => x.Iva);
                _ItemsVats.ICE = _listSales.Sum(x => x.ICE);
                _ItemsVats.IRBPNR = _listSales.Sum(x => x.IRBPNR);


                trib.ambiente = _ambiente;
                trib.tipoEmision = _myCommerce.TIPO_EMISION;
                trib.razonSocial = _myCommerce.RazonSocial;
                trib.nombreComercial = _myCommerce.NameComercial;
                trib.ruc = _myCommerce.Ruc;
                trib.codDoc = EnumTypeDocument.FACTURA.getCode();
                trib.estab = _myCommerce.CodEstablec;
                trib.ptoEmi = _myCommerce.CodPntoEmision;
                trib.secuencial = new string('0', 9 - (_secuancia).ToString().Length) + (_secuancia).ToString();
                trib.dirMatriz = _myCommerce.EstablishmentAddress;

                if (!string.IsNullOrEmpty(_myCommerce.AgenteRetencion))
                    trib.agenteRetencion = _myCommerce.AgenteRetencion;

                if (_myCommerce.IdTypeRegimen == TypeECommerceEnum.Microenterprise)
                    trib.regimenMicroempresas = _myCommerce.RegimenMicroempresas;
                else if (_myCommerce.IdTypeRegimen == TypeECommerceEnum.RIMPE_Taxpayer)
                    trib.contribuyenteRimpe = _myCommerce.ContribuyenteRimpe;



                if (!_ambiente.Contains("-1"))
                {
                    trib.claveAcceso = ClaveAcceso.generarClaveAcceso(
                               _fechaEmision,
                               trib.codDoc,
                               trib.ruc, _ambiente,
                               trib.estab + trib.ptoEmi,
                               trib.secuencial, "12345678", "1");
                }

                claveAcceso = trib.claveAcceso;
                codDdoc = trib.codDoc;
                Num_factura = trib.estab + "-" + trib.ptoEmi + "-" + trib.secuencial;
                return trib;
            });

        }

        private async static Task<facturaInfoFactura> getInfoFactura()
        {
            return await Task.Factory.StartNew(() =>
            {
                facturaInfoFactura inf = new facturaInfoFactura();

                inf.fechaEmision = _fechaEmision.ToString("dd/MM/yyyy");
                inf.dirEstablecimiento = _myCommerce.EstablishmentAddress;
                if (!string.IsNullOrEmpty(_myCommerce.SpecialTaxNumber))
                {
                    inf.contribuyenteEspecial = _myCommerce.SpecialTaxNumber.ToString();
                }

                inf.obligadoContabilidadSpecified = true;
                inf.obligadoContabilidad = _myCommerce.KeepAccounting ? obligadoContabilidad.SI : obligadoContabilidad.NO;
                inf.tipoIdentificacionComprador = IdentificationTypeResolver.ResolveCode( _customer.Personas.Ruc_Ci);

                inf.razonSocialComprador = _customer.Personas.FullName;
                inf.identificacionComprador = _customer.Personas.Ruc_Ci ;
                inf.totalSinImpuestos = _listSales.Sum(x => x.SubTotal);
                inf.totalDescuento = 0;
                inf.importeTotal = Math.Round(_listSales.Sum(x => x.TotalItem), 2);
                inf.moneda = "DOLAR";
                //get list emails
                if (_customer.Personas.SendMail)
                {
                    _emailSend = new List<string>();
                    string emailText = _customer.Personas.Mail;

                    string[] mails = emailText.Split(';');

                    for (int i = 0; i < mails.Length; i++)
                    {
                        if (mails[i].Length > 0 && Funciones.IsValidEmail(mails[i]))
                        {
                            _emailSend.Add(mails[i]);
                        }
                    }
                }


                var lisImp = new List<facturaInfoFacturaTotalImpuesto>();

                foreach (var item in _listSales)
                {
                    var model = new ItemVats();

                    model.ProductId = item.Product.Id;
                    model.baseImponible = item.SubTotal;
                    model.valor = Math.Round(item.Iva + item.ICE + item.IRBPNR, 2);


                    foreach (var item2 in item.Product.PRODUCTO_IMPUESTO)
                    {
                        model.codigo = item2.IMPUESTO_VALOR.CODIGO_IMPUESTO;
                        model.codigoPorcentaje = item2.IMPUESTO_VALOR.CODIGO;
                        model.tarifa = item2.IMPUESTO_VALOR.PORCENTAJE;
                    }

                    _listVats.Add(model);
                }


                lisImp = _listVats.GroupBy(x => x.codigoPorcentaje)
                                .Select(op => new facturaInfoFacturaTotalImpuesto
                                {
                                    codigo = op.FirstOrDefault().codigo,
                                    codigoPorcentaje = op.Key,
                                    baseImponible = op.Sum(x => x.baseImponible),
                                    valor = Math.Round(op.Sum(x => x.valor), 2)
                                }).ToList();



                inf.totalConImpuestos = lisImp.ToArray();

                List<pagosPago> pagos = new List<pagosPago>();


                pagos.Add(new pagosPago
                {
                    formaPago = _formas_pago.CODIGO_FORMA_PAGO,
                    total = Math.Round(_listSales.Sum(x => x.TotalItem), 2),
                    plazo = 0,
                    plazoSpecified = true,
                    unidadTiempo = "Días",
                });

                inf.pagos = pagos.ToArray();
                return inf;
            });

        }

        private async Task<List<facturaDetalle>> getFacturaDetalles()
        {
            return await Task.Factory.StartNew(() =>
            {

                List<facturaDetalle> lisDetall = new List<facturaDetalle>();

                foreach (var item in _listSales)
                {
                    BigDecimal bigDecimal = new BigDecimal(1.000000);
                    decimal result;
                    Decimal.TryParse("0.000000", out result);

                    facturaDetalle facDetail = new facturaDetalle
                    {
                        cantidad = (double)item.Quatity,
                        codigoPrincipal = item.Id.ToString(),
                        codigoAuxiliar = item.Product?.Cod_Secondary == "" ? item.Id.ToString() : item.Product?.Cod_Secondary,
                        descripcion = item.Product.Name_Producto,
                        descuento = 0,
                        precioUnitario = Math.Round(item.Product.UnitPrice, 5),
                        precioTotalSinImpuesto = Math.Round(item.SubTotal, 2),
                        unidadMedida = "UDN",
                    };

                    List<impuesto> lisImp = new List<impuesto>();

                    foreach (var vat in _listVats.Where(x => x.ProductId == item.Product.Id))
                    {
                        lisImp.Add(new impuesto
                        {
                            codigo = vat.codigo,
                            codigoPorcentaje = vat.codigoPorcentaje,
                            baseImponible = vat.baseImponible,
                            tarifa = vat.tarifa,
                            valor = Math.Round((vat.valor), 2),
                        });
                    }

                    facDetail.impuestos = lisImp.ToArray();

                    if (item.Product.INFO_ADICIONALS != null && item.Product.INFO_ADICIONALS.Count > 0)
                    {
                        List<facturaDetalleDetAdicional> ifoAddid = new List<facturaDetalleDetAdicional>();
                        foreach (var infoAdditional in item.Product.INFO_ADICIONALS)
                        {
                            ifoAddid.Add(new facturaDetalleDetAdicional
                            {
                                nombre = infoAdditional.Atribute,
                                valor = infoAdditional.ValueAtribute
                            });
                        }

                        facDetail.detallesAdicionales = ifoAddid.ToArray();
                    }

                    lisDetall.Add(facDetail);

                }

                return lisDetall;
            });
        }
        

        internal InvoiceHeaderInput GetVats()
        {
            return _ItemsVats;
        }

        public string GetClaveAcceso()
        {
            return claveAcceso;
        }

        public string GetNumDocument()
        {
            return Num_factura;
        }


        internal string GetCodDoc()
        {
            return codDdoc;
        }

        internal List<string> GetEmailSend()
        {
            if (_emailSend == null)
            {
                return new List<string>();
            }
            return _emailSend;
        }

        public DateTime GetFechaEmision()
        {
            return _fechaEmision;
        }

        internal factura GetObject()
        {
            return _fact;
        }
        internal int GetIdFactura()
        {
            return _secuancia;
        }

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
            }
            _emailSend = null;
            _fact = null;
            db = null;
            _myCommerce = null;
            _ambiente = null;
            _listSales = null;
            _listVats = null;
        }


    }

    public class ItemVats : facturaInfoFacturaTotalImpuesto
    {
        [Key]
        public int ProductId { get; set; }
    }

}
