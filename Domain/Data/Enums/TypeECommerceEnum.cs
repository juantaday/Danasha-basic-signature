using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Linq;

namespace Domain.Data.Enums
{
    public  enum  TypeECommerceEnum : ushort
    {
        [Display(Name = "No establecido")]
        UNDETERMINED = 0,

        [Display(Name = "Contribuyente especial")]
        SpecialTaxpayer =1,


        [Display(Name = "Contribuyente régimen microempresas")]
        Microenterprise =2,


        [Display(Name = "Contribuyente régimen RIMPE")]
        RIMPE_Taxpayer =3

    }
}
