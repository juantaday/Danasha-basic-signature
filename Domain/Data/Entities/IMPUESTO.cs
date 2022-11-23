using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Data.Entities
{
    [Table ("IMPUESTO", Schema = "sri")]
    public class IMPUESTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]   
        public int CODIGO { get; set; }

        [Required]
        [StringLength (255)]
        public string TAIM_DES_IMP { get; set; }

        [StringLength(1)]
        public string TAIM_ESTADO { get; set; }

        public virtual ICollection <IMPUESTO_VALOR > IMPUESTO_VALORES { get; set; }
    }
}
