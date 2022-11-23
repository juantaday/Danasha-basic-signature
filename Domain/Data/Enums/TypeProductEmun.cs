using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Data.Enums
{

     public  enum TypeProductEmun : ushort
    {
        [Display(Name = "BIEN")]
        Bien = 1,

        [Display(Name = "SERVICIO")]
        Servicio =2
      
    }
}
