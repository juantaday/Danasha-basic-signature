namespace Domain.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Productos", Schema = "dbo")]
    public class Productos
    {
        [Key]
        public int IdProducto { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Nom_Comercial { get; set; } = string.Empty;

        [Required]
        [StringLength(35)]
        [Column(TypeName = "varchar(35)")]
        public string Nom_Comun { get; set; } = string.Empty;

        [StringLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string Descripcion { get; set; }

        [Column(TypeName = "decimal(18, 3)")]
        public decimal Cant_minima { get; set; }

        [Required]
        public int IdUnidad { get; set; }

        [Required]
        public int IdSubCategoria { get; set; }

        public int Stock { get; set; }

        public int Deft_idPresenCompra { get; set; }

        public int Deft_idPresenVenta { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal IvaPorcentaje { get; set; }

        [Required]
        [StringLength(8)]
        [Column(TypeName = "char(8)")]
        public string Coduser { get; set; } = string.Empty;

        [Column(TypeName = "date")]
        public DateTime Fecha_reg { get; set; }

        public bool Facturable { get; set; }

        public bool? Activo { get; set; }

        public int? IdData { get; set; }

        public virtual ICollection<ProductoPresentacion> ProductoPresentaciones { get; set; } = new List<ProductoPresentacion>();   
    }
}
