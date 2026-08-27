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
using System.Linq;
using System.Threading.Tasks;
using obligadoContabilidad = ec.gob.sri.Xml.modelo_v1_1_0.Factura.obligadoContabilidad;

namespace InterfaceSignatureAndSRI.GeneratedXML
{
    public class FacturaXMLWithId : IDisposable
    {
        private static string _ambiente;
        private static List<FacturaVentaDetail_x> _listSales;
        private static List<FacturaVentaImpuesto> _listImpuestos;
        private static List<IMPUESTO_VALOR> _listImpuestosSRI;
        private static SignatureOption _signatureOptions;
        private readonly int _idFactur;
        private readonly int _myCommerceId;
        private static int idFactur;
        private static factura _fact;
        private static DateTime _fechaEmision;
        private static List<string> _emailSend;
        private static Domain.Models.InvoiceHeaderInput _invoiceHeader;
        private static ItemCommerce _Mycomerce;
        private static DomainDataContext db;
        private static string claveAcceso;
        private static string num_factura;
        private static string codDdoc;

        private static List<ItemVats> _listVats;

        private static IMPUESTO_VALOR mPUESTO_VALOR;
        private static FacturaVentaImpuesto ventaImpuestos;

        private static List<ItemsImpuestValor> _qryImpuesto;

        public FacturaXMLWithId(string ambiente, int idFactur_, int myCommerceId)
        {
            db = new DomainDataContext(new DbContextOptions<DataContext>());
            _fact = new factura();
            _ambiente = ambiente;
            _idFactur = idFactur_;
            this._myCommerceId = myCommerceId;
            _listVats = new List<ItemVats>();
            idFactur = idFactur_;

        }

        public async Task<string> GetXmlFactura()
        {
            _fact.version = "1.1.0";
            _fact.id = facturaID.comprobante;
            _fact.idSpecified = true;

            try
            {
                _signatureOptions = db.SignatureOptions.Where(x => x.MyCommerceId == this._myCommerceId).FirstOrDefault();

                _listImpuestosSRI = db.IMPUESTO_VALOR.Where(x => x.CODIGO_IMPUESTO == 2 && x.TIPO_IMPUESTO == "I").ToList();

                _listSales = db.FacturaVentaDetails.Include(x => x.FacturaVentaDiscount)
                    .Include(x => x.ProductoPresentacion).ThenInclude(x => x.Producto)
                    .Where(x => x.IdFacturaVenta == _idFactur).Select(op => new FacturaVentaDetail_x
                    {
                        Cantidad = op.Cantidad,
                        Prec_Venta = op.Prec_Venta,
                        Iva = op.Iva,
                        CodProduct = op.ProductoPresentacion.CodProducto,
                        PresentID = op.IdPresent,
                        ProductId = op.ProductoPresentacion.IdProducto,
                        ProductName = op.ProductoPresentacion.Producto.Nom_Comun,
                        Additional = op.FacturaVentaDiscount == null ? 0 : op.FacturaVentaDiscount.Additional,
                        Discount = op.FacturaVentaDiscount == null ? 0 : op.FacturaVentaDiscount.Discount
                    }).ToList();

                _listImpuestos = db.FacturaVentaImpuestos.Where(x => x.IdFactVenta == _idFactur).ToList();

                _invoiceHeader = db.FacturaVentas
                     .AsNoTracking()
                     .Where(x => x.IdFactVenta == _idFactur)
                     .Select(op => new Domain.Models.InvoiceHeaderInput
                     {
                         IdFactVenta = op.IdFactVenta,
                         WareHouseId = op.IdBodega,
                         CustomerId = op.Clientes.IdCliente,
                         Num_Factu = op.Num_Factu,
                         fechaDesde = op.FechaDesde,
                         BaseIva = op.Base12Iva,
                         ICE = 0,
                         IRBPNR = 0,
                         IVA = op.Iva,
                         Total = op.Total,
                         Customer = new  Domain.Data.Entities.Cliente
                         {
                             IdCliente = op.Clientes.IdCliente,
                             IdPersona = op.Clientes.IdPersona,
                             Credito = op.Clientes.Credito,
                             Monto_Max = op.Clientes.Monto_Max,
                             Personas = op.Clientes.Personas
                         },
                         TotalWithOutVat = System.Math.Round(op.Total - op.Iva, 2, MidpointRounding.AwayFromZero)
                     }).FirstOrDefault();

                _Mycomerce = await FacturaXmlBuilder.GetCommerceAsync(db, _invoiceHeader.WareHouseId);

                // materializar la consulta
                var people = _invoiceHeader.Customer.Personas;

                //sum total discout for facture
                decimal totalDiscout = 0;
                foreach (var item in _listSales)
                {
                    totalDiscout += item.Discount;
                }
                _invoiceHeader.TotalDiscount = totalDiscout;

                //debe existir las dos direcciones de lo contrario el SRI no accepta
                if (string.IsNullOrEmpty(_Mycomerce.AdressCompany) || string.IsNullOrEmpty(_Mycomerce.EstablishmentAddress))
                {
                    throw new Exception("No esta configurado la direccion del local principan o del establecimeiento", new Exception("User_Index"));
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ex.StackTrace, ex.InnerException);
            }
            finally
            {
                if (db != null)
                {
                    if (db.Database.GetDbConnection().State == System.Data.ConnectionState.Open)
                    {
                        db.Database.GetDbConnection().Close();
                    }

                    db.Dispose();
                }
            }

            try
            {

                if (_Mycomerce == null)
                    throw new Exception("No esta configurado el emisor..");

                _fact.infoTributaria = await getInfoTributaria();
                _fact.infoFactura = await getInfoFactura();
                _fact.detalles = await FacturaXmlBuilder.GetFacturaDetalles(_listSales, _qryImpuesto);
                _fact.infoAdicional = await FacturaXmlBuilder.GetInfoAdicional(_invoiceHeader.Customer, _fechaEmision, _invoiceHeader.IdFactVenta);

                return XMLSerializers.Serialize(_fact, "");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        #region Methos internal generated

        private async static Task<infoTributaria> getInfoTributaria()
        {

            return await Task.Factory.StartNew(() =>
            {

                infoTributaria trib = new infoTributaria();


                trib.ambiente = _ambiente;
                trib.tipoEmision = _signatureOptions.TIPO_EMISION;
                trib.razonSocial = _Mycomerce.RazonSocial;
                trib.nombreComercial = _Mycomerce.NameComercial;

                trib.ruc = _Mycomerce.Ruc;
                trib.codDoc = EnumTypeDocument.FACTURA.getCode();
                trib.estab = _invoiceHeader.Num_Factu.Substring(0, 3);
                trib.ptoEmi = _invoiceHeader.Num_Factu.Substring(4, 3);
                trib.secuencial = _invoiceHeader.Num_Factu.Substring(8, 9);
                trib.dirMatriz = _Mycomerce.AdressCompany;

                if (!string.IsNullOrEmpty(_Mycomerce.AgenteRetencion))
                    trib.agenteRetencion = _Mycomerce.AgenteRetencion;

                if (_Mycomerce.IdTypeRegimen == TypeECommerceEnum.Microenterprise) // contribuyente espacial
                    trib.regimenMicroempresas = _Mycomerce.RegimenMicroempresas;
                else if (_Mycomerce.IdTypeRegimen == TypeECommerceEnum.RIMPE_Taxpayer) // contribuyente rimpe (negocio popular, rimpe emprendedor)
                    trib.contribuyenteRimpe = _Mycomerce.ContribuyenteRimpe;

                trib.claveAcceso = ClaveAcceso.generarClaveAcceso(
                               _invoiceHeader.fechaDesde,
                               trib.codDoc,
                               trib.ruc, _ambiente,
                               trib.estab + trib.ptoEmi,
                               trib.secuencial, "12345678", "1");

                codDdoc = trib.codDoc;
                claveAcceso = trib.claveAcceso;
                num_factura = _invoiceHeader.Num_Factu;
                _fechaEmision = _invoiceHeader.fechaDesde;

                return trib;
            });

        }

        private async static Task<facturaInfoFactura> getInfoFactura()
        {
            return await Task.Factory.StartNew(() =>
            {
                facturaInfoFactura inf = new facturaInfoFactura();

                if (_invoiceHeader != null)
                {

                    inf.fechaEmision = _invoiceHeader.fechaDesde.ToString("dd/MM/yyyy");
                    inf.dirEstablecimiento = _Mycomerce.EstablishmentAddress;

                    if (!string.IsNullOrEmpty(_Mycomerce.SpecialTaxNumber))
                    {
                        inf.contribuyenteEspecial = _Mycomerce.SpecialTaxNumber;
                    }

                    inf.obligadoContabilidadSpecified = true;
                    inf.obligadoContabilidad = _Mycomerce.KeepAccounting ? obligadoContabilidad.SI : obligadoContabilidad.NO;
                    inf.tipoIdentificacionComprador =
                           IdentificationTypeResolver.ResolveCode(_invoiceHeader.Customer.Personas.Ruc_Ci.ToUpper().Trim());

                    inf.razonSocialComprador = _invoiceHeader.Customer.Personas.FullName;
                    inf.identificacionComprador = _invoiceHeader.Customer.Personas.Ruc_Ci;
                    inf.totalSinImpuestos = _invoiceHeader.TotalWithOutVat;
                    inf.totalDescuento = _invoiceHeader.TotalDiscount;
                    inf.importeTotal = _invoiceHeader.Total;
                    inf.moneda = _Mycomerce.Moneda;
                    //get list emails
                    if (_invoiceHeader.Customer.Personas.SendMail)
                    {
                        _emailSend = new List<string>();
                        string[] mails = _invoiceHeader.Customer.Personas.Mail.Split(';');
                        for (int i = 0; i < mails.Length; i++)
                        {
                            if (Funciones.IsValidEmail(mails[i]))
                            {
                                _emailSend.Add(mails[i]);
                            }
                        }
                    }

                    _qryImpuesto = _listImpuestos.GroupJoin(_listImpuestosSRI,
                          im => (im.IvaPorcentaje * 100),
                          sr => sr.PORCENTAJE,
                           (f, bs) => new { imp = f, ivSri = bs.FirstOrDefault() })
                    .Select(op => new ItemsImpuestValor
                    {
                        BaseImponible = op.imp.BaseImponible,
                        CODIGO = op.ivSri.CODIGO_IMPUESTO,
                        CODIGO_TIPO_IMPUESTO = op.ivSri.CODIGO,
                        PORCENTAJE = op.ivSri.PORCENTAJE,
                        Valor = op.imp.Valor,
                        PresentId = op.imp.IdPresent
                    }).ToList();

                    var groupedSum = _qryImpuesto.GroupBy(x => x.CODIGO_TIPO_IMPUESTO).Select(x => new
                    {
                        BaseImponible = x.Sum(s => s.BaseImponible),
                        Valor = x.Sum(g => g.Valor),
                        CODIGO = x.FirstOrDefault().CODIGO,
                        CODIGO_TIPO_IMPUESTO = x.Key,
                        PORCENTAJE = x.FirstOrDefault().PORCENTAJE
                    }).ToList();

                    List<facturaInfoFacturaTotalImpuesto> lisImp = new List<facturaInfoFacturaTotalImpuesto>();

                    foreach (var item in groupedSum)
                    {
                        lisImp.Add(new facturaInfoFacturaTotalImpuesto
                        {
                            codigo = item.CODIGO,
                            codigoPorcentaje = item.CODIGO_TIPO_IMPUESTO,
                            baseImponible = item.BaseImponible,
                            valor = item.Valor,
                        });
                    }



                    inf.totalConImpuestos = lisImp.ToArray();

                    List<pagosPago> pagos = new List<pagosPago>();
                    //new EnumTypePago("SIN UTILIZACION DEL SISTEMA FINANCIERO", 1, "01");
                    string codPago = "01";
                    pagos.Add(new pagosPago
                    {
                        formaPago = codPago,
                        total = _invoiceHeader.Total,
                        plazo = codPago == "01" ? 0 : 30,
                        plazoSpecified = true,
                        unidadTiempo = "Días",
                    });
                    inf.pagos = pagos.ToArray();
                }
                return inf;
            });

        }

    
        #endregion

        #region Internal methos

        internal DateTime GetFechaEmision()
        {
            return _fechaEmision;
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

        internal InvoiceHeaderInput GetVats()
        {
            return _invoiceHeader;
        }

        internal string GetClaveAcceso()
        {
            return claveAcceso;
        }

        internal int GetIdFactura()
        {
            return idFactur;
        }

        internal string GetNumDocumet()
        {
            return num_factura;
        }

        #endregion

        public void Dispose()
        {
            if (db != null)
                db.Dispose();

            db = null;
            _fact = null;
            _listSales = null;
            _listImpuestos = null;
            _listImpuestosSRI = null;
            _signatureOptions = null;
            _emailSend = null;
            num_factura = null;
            idFactur = -1;
        }


    }

    public class FacturaVentaDetail_x
    {
        public int PresentID { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CodProduct { get; set; }
        public decimal Discount { get; set; }
        public decimal Additional { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Prec_Venta { get; set; }
        public decimal Iva { get; set; }

    }

}
