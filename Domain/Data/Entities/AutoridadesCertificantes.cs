using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Data.Entities
{
    [Table("AutoridadesCertificantes", Schema = "Sng")]
    public class AutoridadesCertificante
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        [Display(Name = "Nombre común")]
        [Required]
        [StringLength(250)]
        public string CN { get; set; }

        /// <summary>
        /// Unidad organizativa
        /// </summary>
        [Required]
        [StringLength(250)]
        [Display(Name = "Nombre de la unidad organizativa")]
        public string OU { get; set; }

        /// <summary>
        /// Nombre de la organización
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre de la organización")]
        public string O { get; set; }


        /// <summary>
        /// Country
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "País")]
        public string C { get; set; }


        /// <summary>
        /// ID de organizacion
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "ID de organizacion")]
        public string  OID { get; set; }

    }
}
