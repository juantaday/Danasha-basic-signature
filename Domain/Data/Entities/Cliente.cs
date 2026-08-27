namespace Domain.Data.Entities
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Clientes", Schema = "dbo")]
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        public int IdPersona { get; set; }

        public bool Credito { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Monto_Max { get; set; }

        public virtual Personas Personas { get; set; }

        public virtual ICollection<FacturaVenta> FacturaVentas { get; set; }   
    }
}
