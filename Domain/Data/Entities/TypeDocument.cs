using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Data.Entities
{
    [Table ("TypeDocuments", Schema ="stm")]
    public  class TypeDocument
    {
        [Key]
        public int  Id { get; set; }

        public string   NameDocument { get; set; }

        public int Numeration { get; set; } =1; 
    }
}
