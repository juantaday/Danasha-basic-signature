namespace Domain.Data.Entities
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Personas", Schema = "dbo")]
    public class Personas
    {
        [Key]
        public int IdPersona { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Apellidos { get; set; } = string.Empty;

        [StringLength(60)]
        [Column(TypeName = "varchar(60)")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(13)]
        [Column(TypeName = "varchar(13)")]
        public string Ruc_Ci { get; set; } = string.Empty;

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Direccion { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Telefono { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_reg { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Mail { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fech_Naci { get; set; }

        public bool Genero { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Nota { get; set; }

        [Column(TypeName = "image")]
        public byte[] Foto { get; set; }

        [StringLength(12)]
        [Column(TypeName = "varchar(12)")]
        public string Telef_casa { get; set; }

        [StringLength(12)]
        [Column(TypeName = "varchar(12)")]
        public string Telef_ofic { get; set; }

        public bool SendMail { get; set; }

        [Required]
        public int PersonTypeId { get; set; }
        public string FullName => $"{Apellidos} {Nombre}";  
       
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
