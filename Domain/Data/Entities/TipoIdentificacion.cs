using Domain.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Entities
{
    public class TypeIdentification
    {
        public int Id { get; set; }

        [StringLength(1)]
        [Required]
        public string Codigo { get; set; }


        [StringLength(50)]
        [Required]
        public string Descrip { get; set; }

        [StringLength(2)]
        [Required]
        public string Codigo_SRI { get; set; }

        public int Dimension { get; set; }

        public TypeFiedEnum TypeFied { get; set; }

    }
}
