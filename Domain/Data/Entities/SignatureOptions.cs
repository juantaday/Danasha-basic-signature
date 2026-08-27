using Domain.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [StringLength(40)]
        public string TOKEN { get; set; }

        /// <summary>
        /// Heulla dijital de la firma
        /// Esta velor se ecuentra en en la propiedades de la firma.
        /// </summary>
        [StringLength(64)]
        public string THUMBPRINT { get; set; }

        [StringLength(255)]
        public string RUTA_ARCHIVO { get; set; }

        public virtual MyCommerce MyCommerce { get; set; }

    }
}
