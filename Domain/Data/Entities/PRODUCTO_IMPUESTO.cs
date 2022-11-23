using Domain.Data.Enums;
using Microsoft.EntityFrameworkCore.Query.ResultOperators.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain.Data.Entities
{
    public class PRODUCTO_IMPUESTO
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        [Required]
        [StringLength(255)]
        public string CODIGO_IMPUESTO { get; set; }

        [Required]
        public int TIPO_IMPUESTO { get; set; }

        public virtual  Product Product { get; set; }

        public virtual IMPUESTO_VALOR IMPUESTO_VALOR { get; set; }

    }
}
