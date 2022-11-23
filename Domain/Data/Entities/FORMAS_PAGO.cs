using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Data.Entities
{
    [Table ("FORMAS_PAGOS", Schema = "sri")]
    public  class FORMAS_PAGO
    {
        [Key ]
        public int Id { get; set; }

        [StringLength (2)]
        [Required]
        public string CODIGO_FORMA_PAGO { get; set; }

        [StringLength(100)]
        [Required]
        public string DESCRIPCION { get; set; }

        [Required]
        [Column (TypeName ="Date")]
        public DateTime FECHA_INICIO  { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? FECHA_FIN { get; set; }

    }
}
