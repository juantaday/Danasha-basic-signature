using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Domain.Data.Enums
{
    public enum TipoAmbienteEnum :ushort
    {
        [Display(Name = "PRUEBAS")]
        PRUEBAS = 1,

        [Display(Name = "PRODUCCION")]
        PRODUCCION = 2
    }


    

}
