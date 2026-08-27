namespace Domain.Data.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductoPresentacion", Schema = "dbo")]
    public class ProductoPresentacion
    {
        [Key]
        public int IdPresentacion { get; set; }

        [Required]
        [StringLength(40)]
        [Column(TypeName = "varchar(40)")]
        public string CodProducto { get; set; } = string.Empty;

        [Required]
        public int IdProducto { get; set; }

        [Required]
        public int IdProUndMed { get; set; }

        public int? IdProUndReferen { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cant_Present { get; set; }

        [Column(TypeName = "decimal(30, 6)")]
        public decimal PrecioCompra { get; set; }

        [Column(TypeName = "decimal(20, 5)")]
        public decimal PrecioVenta { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fech_UltimaCompra { get; set; }

        [Required]
        [StringLength(8)]
        [Column(TypeName = "char(8)")]
        public string CodUser { get; set; } = string.Empty;

        [Column(TypeName = "decimal(30, 3)")]
        public decimal? Empaquetado { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Presentacion { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string PresentacionPrint { get; set; }

        public bool IsPresentFactory { get; set; }

        [StringLength(25)]
        [Column(TypeName = "char(25)")]
        public string Barcode { get; set; }

        public virtual Productos Producto { get; set; } = new Productos();  

        public virtual ICollection<FacturaVentaDetail> FacturaVentaDetails { get; set; }
    }
}
