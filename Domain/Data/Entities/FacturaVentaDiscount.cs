using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Data.Entities
{
    [Table("FacturaVentaDiscount", Schema = "dbo")]
    public class FacturaVentaDiscount
    {
        [Key, ForeignKey(nameof(FacturaVentaDetail))]
        public int IdFacturVentaDetail { get; set; }

        [Required]
        [StringLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string CodUserAuthorize { get; set; } = string.Empty;

        [Column(TypeName = "decimal(5, 3)")]
        public decimal Discount { get; set; }

        [Column(TypeName = "decimal(5, 3)")]
        public decimal Additional { get; set; }

        // Propiedad de navegación hacia la entidad principal
        public virtual FacturaVentaDetail FacturaVentaDetail { get; set; }
    }
}
