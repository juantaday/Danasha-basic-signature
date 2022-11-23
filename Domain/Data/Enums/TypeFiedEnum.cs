using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Data.Enums
{
    public enum TypeFiedEnum : ushort
    {
        [Display (Name ="NUMÉRICO")]
        INTEGER  =1,

        [Display(Name = "FECHA")]
        DATE =2,

        [Display(Name = "TEXTO")]
        VARCHAR =3, 

    }
}
