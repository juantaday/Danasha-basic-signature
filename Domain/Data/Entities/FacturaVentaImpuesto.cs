using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Data.Entities
{
    [Table("FacturaVentaImpuestos")]
    public class FacturaVentaImpuesto
    {
        [Key]
        public int Id { get; set; }

        public int IdFactVenta { get; set; }

        public int IdPresent { get; set; }

        public decimal? IvaPorcentaje { get; set; }

        public decimal BaseImponible { get; set; }

        public decimal Valor { get; set; }

        public virtual FacturaVenta FacturaVenta { get; set; }  = new FacturaVenta();   

    }
}
