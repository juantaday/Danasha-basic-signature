using System.ComponentModel.DataAnnotations;

namespace Domain.Data.Enums
{
    public enum TypeECommerceEnum : ushort
    {
        [Display(Name = "Régimen general")]
        Regimen_General = 0,

        [Display(Name = "Contribuyente especial")]
        SpecialTaxpayer = 1,


        [Display(Name = "Contribuyente régimen microempresas")]
        Microenterprise = 2,


        [Display(Name = "Contribuyente régimen RIMPE")]
        RIMPE_Taxpayer = 3

    }
}
