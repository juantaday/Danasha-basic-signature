namespace Domain.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FacturaVentaDetail", Schema = "dbo")]
    public class FacturaVentaDetail
    {
        [Key]
        public int IdFacturVentaDetail { get; set; }

        [Required]
        public int IdFacturaVenta { get; set; }

        [Required]
        public int IdPresent { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cantidad { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Prec_Venta { get; set; }

        [Column(TypeName = "decimal(12, 5)")]
        public decimal Iva { get; set; }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal Prec_Compra { get; set; }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal Prec_Prome_Bodega { get; set; }

        public virtual FacturaVenta FacturaVenta { get; set; } = new FacturaVenta();

        public virtual FacturaVentaDiscount FacturaVentaDiscount { get; set; }

        public virtual ProductoPresentacion ProductoPresentacion  { get; set; }
    }
}
