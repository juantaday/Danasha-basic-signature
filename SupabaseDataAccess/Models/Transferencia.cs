using System;
using System.Collections.Generic;

namespace SupabaseDataAccess.Models
{
    /// <summary>
    /// Mapea la tabla 'transferencias' de Supabase.
    /// </summary>
    public class Transferencia
    {
        public Guid Id { get; set; }
        public string NumTransferencia { get; set; }
        public int BodegaOrigenId { get; set; }
        public string BodegaOrigenNom { get; set; }
        public int BodegaDestinoId { get; set; }
        public string BodegaDestinoNom { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Estado { get; set; }  // PENDIENTE | RECIBIDO | CON_NOVEDAD
        public string Novedad { get; set; }
        public DateTime? FechaRecepcion { get; set; }

        // El detalle viene como JSON y se deserializa a esta lista
        public List<DetalleTransferencia> Detalle { get; set; } = new List<DetalleTransferencia>();
    }

    public class DetalleTransferencia
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal CantidadEnviada { get; set; }
        public decimal? CantidadRecibida { get; set; }
        public string Unidad { get; set; }
    }
}
