using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Data.Entities
{
    [Table("FacturaVenta")]
    public class FacturaVenta
    {
        [Key]
        public int IdFactVenta { get; set; }

        public string Num_Factu { get; set; } = string.Empty;

        public int IdCliente { get; set; }

        public int IdFormaPago { get; set; }

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }

        public decimal Base00Iva { get; set; }

        public decimal Base12Iva { get; set; }

        public decimal Iva { get; set; }

        public decimal Total { get; set; }

        public string CodIus { get; set; } = string.Empty;

        public bool Impreso { get; set; }

        public byte Estado { get; set; }

        public int? IdTipoDocument { get; set; }

        public decimal OtroValor { get; set; }

        public int IdBodega { get; set; }

        public string CodTerminal { get; set; } = string.Empty;

        public virtual Cliente Clientes { get; set; }

        public virtual ICollection<FacturaVentaImpuesto> FacturaVentaImpuestos { get; set; }

        public virtual ICollection<FacturaVentaDetail> FacturaVentaDetails { get; set; }
    }

}
