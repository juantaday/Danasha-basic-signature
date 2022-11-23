using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Data.Entities
{
    [Serializable]
    public  class INFO_ADICIONAL
    {
        public int Id { get; set; }

        public int ProductId { get; set; }


        [Required]
        [StringLength (30)]
        public string Atribute { get; set; }


        [Required]
        [StringLength(50)]
        public string ValueAtribute { get; set; }
        
        [NotMapped ]
        public virtual int  Order { get; set; }
       
        public virtual Product Product { get; set; }

    }
}
