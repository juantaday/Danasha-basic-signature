using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Data.Enums
{
    public  enum TypeClienteEnum
    {
        [Display (Name = "CLIENTE")]
        CLIENTE = 1,

        [Display(Name = "SUJETO RETENIDO")]
        SUBJECT = 2,

        [Display(Name = "DESTINATARIO")]
        FROM = 3,

    }
}
