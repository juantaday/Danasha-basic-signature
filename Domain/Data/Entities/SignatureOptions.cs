using Domain.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Domain.Data.Entities
{
    [Table("SignatureOptions", Schema = "cmc")]
    public class SignatureOption
    {
    
        [Key]
        public int Id { get; set; }
        public int MyCommerceId { get; set; }

        /// <summary>
        /// Para el método de autorización offline, solo existe el tipo de emisión normal [1]
        /// </summary>
        [StringLength(1)]
        [Required]
        public string TIPO_EMISION { get; set; } = "1";

        [Required]
        public byte TIEMPO_ESPERA { get; set; } = 3;

        [StringLength(250)]
        public string CLAVE_INTERNA { get; set; }

   
        [Required]
        public TipoAmbienteEnum TIPO_AMBIENTE { get; set; }

        /// <summary>
        /// Tipo de token para firmar...
        /// </summary>
        [StringLength(30)]
        public string TOKEN { get; set; }

        [StringLength(255)]
        public string RUTA_ARCHIVO { get; set; }

        public virtual MyCommerce MyCommerce { get; set; }

    }
}
