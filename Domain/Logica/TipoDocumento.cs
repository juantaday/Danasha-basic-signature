using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Logica
{
    public enum TipoDocumento
    {
        [Display(Name = "Factura")]
        Factura = 1,

        [Display(Name = "Recibo")]
        Recibo = 2,

        [Display(Name = "Nota de venta")]
        NotaDeVenta = 3,

        [Display(Name = "Proforma")]
        Proforma = 6,

        [Display(Name = "Guía de remisión")]
        GuiaDeRemision = 7,

        [Display(Name = "Pedido")]
        Pedido = 8,

        [Display(Name = "Reporte de cierre de caja")]
        ReporteCierreCaja = 9,

        [Display(Name = "Recibo cobro deuda cliente")]
        ReciboCobroDeudaCliente = 10
    }
}