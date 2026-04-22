using CADsisVenta;
using CADsisVenta.Data;
using Domain.Data.Enums;
using Domain.Models;
using ec.gob.sri.comprobantes.Enum;
using ec.gob.sri.comprobantes.Utils;
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
        private static List<FacturaVentaImpuestos> _listImpuestos;
        private static List<CADsisVenta.IMPUESTO_VALOR> _listImpuestosSRI;
        private static SignatureOptions _signatureOptions;
        private readonly int _idFactur;
        private readonly int _myCommerceId;
        private static int idFactur;
        private static factura _fact;
        private static DateTime _fechaEmision;
        private static List<string> _emailSend;
        private static Domain.Models.ItemsVats _ItemsVat;
        private static ItemCommerce _Mycomerce;
        private static DataContext db;
        private static string claveAcceso;
        private static string num_factura;
        private static string codDdoc;

        private static List<ItemVats> _listVats;

        private static IMPUESTO_VALOR mPUESTO_VALOR;
        private static FacturaVentaImpuestos ventaImpuestos;

        private static List<ItemsImpuestValor> _qryImpuesto;

        public FacturaXMLWithId(string ambiente, int idFactur_, int myCommerceId)
        {
            db = new DataContext();
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
                _Mycomerce = db.myCommerce.Select(op => new ItemCommerce
                {
                    Id = op.CommerceId,
                    AdressCompany = op.Domicilio,
                    CellPhone = op.CellPhone,
                    Phone = op.Phone,
                    Ruc = op.Ruc,
                    NameComercial = op.NameComercial,
                    RazonSocial = op.RazonSocial,
                    NameCompay = op.Company,
                    AgenteRetencion = op.AgenteRetencion,
                    IdTypeRegimen = op.IdTypeRegimen,
                    SpecialTaxNumber = op.SpecialTaxNumber,
                    KeepAccounting = op.KeepAccounting,
                    Moneda = op.TypoMonedaDecrip,
                    RegimenMicroempresas = op.RegimenMicroempresas,
                    ContribuyenteRimpe = op.ContribuyenteRimpe    
                }).FirstOrDefault();


                _signatureOptions = db.SignatureOptions.Where(x => x.MyCommerceId == this._myCommerceId).FirstOrDefault();

                _listImpuestosSRI = db.IMPUESTO_VALOR.Where(x => x.CODIGO_IMPUESTO == 2 && x.TIPO_IMPUESTO == "I").ToList();

                _listSales = db.FacturaVentaDetail.Include(x => x.FacturaVentaDiscount)
                    .Include(x => x.ProductoPresentacion).ThenInclude(x => x.Productos)
                    .Where(x => x.idFacturaVenta == _idFactur).Select(op => new FacturaVentaDetail_x
                    {
                        Cantidad = op.Cantidad,
                        Prec_Venta = op.Prec_Venta,
                        Iva = op.Iva,
                        CodProduct = op.ProductoPresentacion.codProducto,
                        PresentID = op.idPresent,
                        ProductId = op.ProductoPresentacion.idProducto,
                        ProductName = op.ProductoPresentacion.Productos.Nom_Comun,
                        Additional = op.FacturaVentaDiscount == null ? 0 : op.FacturaVentaDiscount.Additional,
                        Discount = op.FacturaVentaDiscount == null ? 0 : op.FacturaVentaDiscount.Discount
                    }).ToList();

                _listImpuestos = db.FacturaVentaImpuestos.Where(x => x.IdFactVenta == _idFactur).ToList();

                _ItemsVat = db.FacturaVenta.Include(c => c.Clientes).ThenInclude(p => p.Personas)
                         .Where(x => x.idFactVenta == _idFactur).Select(op => new Domain.Models.ItemsVats
                         {
                             WareHouseId = op.idBodega,
                             Phone = op.Clientes.Personas.telefono,
                             AddressCustomer = op.Clientes.Personas.Direccion,
                             Ruc = op.Clientes.Personas.Ruc_Ci,
                             FullName = op.Clientes.Personas.Apellidos + " " + op.Clientes.Personas.Nombre,
                             Emails = op.Clientes.Personas.mail,
                             CustomerId = op.Clientes.idCliente,
                             SendEmail = op.Clientes.Personas.SendMail,
                             Num_Factu = op.Num_Factu,
                             fechaDesde = op.fechaDesde,
                             BaseIva = op.Base00Iva,
                             ICE = 0,
                             IRBPNR = 0,
                             IVA = op.Iva,
                             Total = op.Total,
                             TotalWithOutVat = System.Math.Round(op.Total - op.Iva, 2, MidpointRounding.AwayFromZero)
                         }).FirstOrDefault();

                //sum total discout for facture
                decimal totalDiscout = 0;
                foreach (var item in _listSales)
                {
                    totalDiscout += item.Discount;
                }
                _ItemsVat.TotalDiscount = totalDiscout;


                var adreeLocal = db.Bodegas.Where(x => x.idBodega == _ItemsVat.WareHouseId).FirstOrDefault();
                if (adreeLocal != null)
                    _Mycomerce.EstablishmentAddress = adreeLocal.Direc_Bodega;

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
                    if (db.Connection.State == System.Data.ConnectionState.Open)
                        db.Connection.Close();

                    db.Dispose();
                }
            }

            try
            {

                if (_Mycomerce == null)
                    throw new Exception("No esta configurado el emisor..");

                _fact.infoTributaria = await getInfoTributaria();
                _fact.infoFactura = await getInfoFactura();
                _fact.detalles = await getFacturaDetalles();
                _fact.infoAdicional = await getInfoAdicional();

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
                trib.estab = _ItemsVat.Num_Factu.Substring(0, 3);
                trib.ptoEmi = _ItemsVat.Num_Factu.Substring(4, 3);
                trib.secuencial = _ItemsVat.Num_Factu.Substring(8, 9);
                trib.dirMatriz = _Mycomerce.AdressCompany;

                if (!string.IsNullOrEmpty(_Mycomerce.AgenteRetencion))
                    trib.agenteRetencion = _Mycomerce.AgenteRetencion;

                if (_Mycomerce.IdTypeRegimen == 2) // contribuyente espacial
                    trib.regimenMicroempresas = _Mycomerce.RegimenMicroempresas;
                else if (_Mycomerce.IdTypeRegimen == 3) // contribuyente rimpe (negocio popular, rimpe emprendedor)
                    trib.contribuyenteRimpe = _Mycomerce.ContribuyenteRimpe;    

                trib.claveAcceso = ClaveAcceso.generarClaveAcceso(
                               _ItemsVat.fechaDesde,
                               trib.codDoc,
                               trib.ruc, _ambiente,
                               trib.estab + trib.ptoEmi,
                               trib.secuencial, "12345678", "1");

                codDdoc = trib.codDoc;
                claveAcceso = trib.claveAcceso;
                num_factura = _ItemsVat.Num_Factu;
                _fechaEmision = _ItemsVat.fechaDesde;

                return trib;
            });

        }

        private async static Task<facturaInfoFactura> getInfoFactura()
        {
            return await Task.Factory.StartNew(() =>
            {
                facturaInfoFactura inf = new facturaInfoFactura();

                if (_ItemsVat != null)
                {

                    inf.fechaEmision = _ItemsVat.fechaDesde.ToString("dd/MM/yyyy");
                    inf.dirEstablecimiento = _Mycomerce.EstablishmentAddress;

                    if (!string.IsNullOrEmpty(_Mycomerce.SpecialTaxNumber))
                    {
                        inf.contribuyenteEspecial = _Mycomerce.SpecialTaxNumber;
                    }

                    inf.obligadoContabilidadSpecified = true;
                    inf.obligadoContabilidad = _Mycomerce.KeepAccounting ? obligadoContabilidad.SI : obligadoContabilidad.NO;
                    inf.tipoIdentificacionComprador =
                             TypeIdentification.GetTypeIdentification(_ItemsVat.Ruc.ToUpper().Trim());

                    inf.razonSocialComprador = _ItemsVat.FullName;
                    inf.identificacionComprador = _ItemsVat.Ruc;
                    inf.totalSinImpuestos = _ItemsVat.TotalWithOutVat;
                    inf.totalDescuento = _ItemsVat.TotalDiscount;
                    inf.importeTotal = _ItemsVat.Total;
                    inf.moneda = _Mycomerce.Moneda;
                    //get list emails
                    if (_ItemsVat.SendEmail)
                    {
                        _emailSend = new List<string>();
                        string[] mails = _ItemsVat.Emails.Split(';');
                        for (int i = 0; i < mails.Length; i++)
                        {
                            if (Funciones.IsValidEmail(mails[i]))
                            {
                                _emailSend.Add(mails[i]);
                            }
                        }
                    }



                    _qryImpuesto = _listImpuestos.GroupJoin(_listImpuestosSRI,
                          im => (im.ivaPorcentaje * 100),
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
                        total = _ItemsVat.Total,
                        plazo = codPago == "01" ? 0 : 30,
                        plazoSpecified = true,
                        unidadTiempo = "Días",
                    });
                    inf.pagos = pagos.ToArray();
                }
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



                    foreach (var vat in _qryImpuesto.Where(x => x.PresentId == item.PresentID))
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

        private async static Task<facturaCampoAdicional[]> getInfoAdicional()
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
                    nombre = "Cod.Cliente",
                    Value = _ItemsVat.CustomerId.ToString()
                });

                infAdi.Add(new facturaCampoAdicional
                {
                    nombre = "Dirección",
                    Value = string.IsNullOrEmpty(_ItemsVat.AddressCustomer) ?
                          "Naranjito" : _ItemsVat.AddressCustomer
                });


                infAdi.Add(new facturaCampoAdicional
                {
                    nombre = "Teléfono",
                    Value = string.IsNullOrEmpty(_ItemsVat.Phone) ?
                            "-" : _ItemsVat.Phone
                });

                infAdi.Add(new facturaCampoAdicional
                {
                    nombre = "Nro Interno",
                    Value = idFactur.ToString()
                });
                return infAdi.ToArray();
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

        internal ItemsVats GetVats()
        {
            return _ItemsVat;
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
