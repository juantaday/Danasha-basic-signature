using Domain.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Data.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        [StringLength(70)]
        [Required]
        public string Nombre { get; set; }

        [Required]
        public int TypyIdentificationId { get; set; }

        [StringLength(30)]
        [Required ]
        public string Num_Identity { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength (15)]
        public string PhoneConvencional { get; set; }

        [StringLength(15)]
        public string  Phone { get; set; }

        [EmailAddress]
        public string MainEmail { get; set; }

        [EmailAddress]
        public string AlternativeEmail { get; set; }

        public bool  SendMails { get; set; } = false;

        public TypeClienteEnum TypeCliente { get; set; }

        public virtual TypeIdentification TypeIdentification { get; set; }

    }
}
