using Domain.Data.Enums;
using System;

namespace Domain.Models
{
    public class InvoiceHeaderInput
    {
        public int IdFactVenta { get; set; }
        public int CustomerId { get; set; }
        public int WareHouseId { get; set; }

        public string Num_Factu { get; set; }

        public DateTime fechaDesde { get; set; }

        public decimal Total { get; set; }
        public decimal TotalWithOutVat { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal BaseIva { get; set; }
        public decimal IVA { get; set; }
        public decimal ICE { get; set; }
        public decimal IRBPNR { get; set; }

        public Domain.Data.Entities.Cliente Customer { get; set; }
    }

    public class ItemCommerce
    {

        public string TIPO_EMISION;

        public int Id { get; set; }
        public string Ruc { get; set; }
        public TypeECommerceEnum IdTypeRegimen { get; set; }
        public string NameCompay { get; set; }
        public string NameComercial { get; set; }
        public string RazonSocial { get; set; }
        public string AgenteRetencion { get; set; }
        public string AdressCompany { get; set; }
        public string EstablishmentAddress { get; set; }
        public string CodEstablecimiento { get; set; }
        public string SpecialTaxNumber { get; set; }

        public bool KeepAccounting { get; set; }

        public string Moneda { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string RegimenMicroempresas { get; set; }
        public string ContribuyenteRimpe { get; set; }
        public string CodEstablec { get; set; }
        public string CodPntoEmision { get; set; }
    }

    public class ItemsImpuestValor
    {
        public int PresentId { get; set; }
        public decimal BaseImponible { get; set; }

        public decimal Valor { get; set; }

        public int CODIGO { get; set; }

        public string CODIGO_TIPO_IMPUESTO { get; set; }

        public decimal PORCENTAJE { get; set; }

    }

}
