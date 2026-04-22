using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ItemsVats
    {
        public int CustomerId { get; set; }
        public int WareHouseId { get; set; }
        public string Ruc { get; set; }
        public string FullName { get; set; }
        /// <summary>
        /// company address
        /// </summary>
        public string AddressCustomer { get; set; }
        /// <summary>
        /// address of the establishment
        /// </summary>
        public string  Phone { get; set; }
        public string  Emails { get; set; }
        public bool  SendEmail { get; set; }
        public string Num_Factu { get; set; }

        public DateTime fechaDesde { get; set; }

        public decimal Total { get; set; }
        public decimal TotalWithOutVat { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal BaseIva { get; set; }
        public decimal IVA { get; set; }
        public decimal ICE { get; set; }
        public decimal IRBPNR { get; set; }
    }

    public class ItemCommerce {
        public int Id { get; set; }
        public string  Ruc  { get; set; }
        public byte IdTypeRegimen { get; set; }
        public string NameCompay { get; set; }
        public string NameComercial { get; set; }
        public string RazonSocial { get; set; }
        public string AgenteRetencion { get; set; }
        public string AdressCompany { get; set; }
        public string EstablishmentAddress { get; set; }
        public string SpecialTaxNumber { get; set; }

        public bool KeepAccounting { get; set; }

        public string Moneda { get; set; }
        public string  Phone { get; set; }
        public string CellPhone { get; set; }  
        public string RegimenMicroempresas { get; set; } 
        public string ContribuyenteRimpe { get; set; }  

    }

    public class ItemsImpuestValor
    {
        public int PresentId { get; set; }
        public decimal BaseImponible { get; set; }

        public decimal Valor { get; set; }

        public int  CODIGO { get; set; }

        public string CODIGO_TIPO_IMPUESTO { get; set; }

        public decimal  PORCENTAJE { get; set; }
 
    }

}
