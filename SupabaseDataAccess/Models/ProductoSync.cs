using System;

namespace SupabaseDataAccess.Models
{
    /// <summary>
    /// Mapea la tabla 'productos_sync' de Supabase.
    /// Permite sincronizar productos nuevos desde Inés María hacia Matilde.
    /// </summary>
    public class ProductoSync
    {
        public Guid Id { get; set; }
        public int IdProductoOrig { get; set; }
        public string NomComercial { get; set; }
        public string NomComun { get; set; }
        public string Descripcion { get; set; }
        public decimal CantMinima { get; set; } = 1;
        public int IdUnidad { get; set; } = 1;
        public int? IdSubcategoria { get; set; }
        public decimal IvaPorcentaje { get; set; }
        public bool Facturable { get; set; } = true;
        public string CodProducto { get; set; }
        public decimal CantPresent { get; set; } = 1;
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public string UnidadPresent { get; set; }
        public string EstadoSync { get; set; } = "PENDIENTE";
        public DateTime FechaCreacion { get; set; }
    }
}
