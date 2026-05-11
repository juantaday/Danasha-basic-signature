using Npgsql;
using SupabaseDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupabaseDataAccess.Repositories
{
    public static class ProductoSyncRepository
    {
        /// <summary>
        /// Obtiene los datos de sincronización de un producto desde Supabase.
        /// Retorna null si no existe.
        /// </summary>
        public static ProductoSync ObtenerPorIdOrigen(int idProductoOrig)
        {
            const string sql = @"
                SELECT id, id_producto_orig, nom_comercial, nom_comun, descripcion,
                       cant_minima, id_unidad, id_subcategoria, iva_porcentaje,
                       facturable, cod_producto, cant_present, precio_compra,
                       precio_venta, unidad_present, estado_sync, fecha_creacion
                FROM   productos_sync
                WHERE  id_producto_orig = @id AND estado_sync = 'PENDIENTE'
                LIMIT  1;";

            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("id", idProductoOrig);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    return new ProductoSync
                    {
                        Id = reader.GetGuid(0),
                        IdProductoOrig = reader.GetInt32(1),
                        NomComercial = reader.GetString(2),
                        NomComun = reader.IsDBNull(3) ? null : reader.GetString(3),
                        Descripcion = reader.IsDBNull(4) ? null : reader.GetString(4),
                        CantMinima = reader.IsDBNull(5) ? 1m : reader.GetDecimal(5),
                        IdUnidad = reader.IsDBNull(6) ? 1 : reader.GetInt32(6),
                        IdSubcategoria = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                        IvaPorcentaje = reader.IsDBNull(8) ? 0m : reader.GetDecimal(8),
                        Facturable = !reader.IsDBNull(9) && reader.GetBoolean(9),
                        CodProducto = reader.IsDBNull(10) ? null : reader.GetString(10),
                        CantPresent = reader.IsDBNull(11) ? 1m : reader.GetDecimal(11),
                        PrecioCompra = reader.IsDBNull(12) ? 0m : reader.GetDecimal(12),
                        PrecioVenta = reader.IsDBNull(13) ? 0m : reader.GetDecimal(13),
                        UnidadPresent = reader.IsDBNull(14) ? "UN" : reader.GetString(14),
                        EstadoSync = reader.GetString(15),
                        FechaCreacion = reader.GetDateTime(16)
                    };
                }
            }
        }

        /// <summary>
        /// Obtiene los datos de sincronización de varios productos desde Supabase.
        /// </summary>
        public static List<ProductoSync> ObtenerPorIdsOrigen(IEnumerable<int> idsProductoOrig)
        {
            if (idsProductoOrig == null)
                return new List<ProductoSync>();

            var ids = idsProductoOrig.Distinct().ToArray();
            if (ids.Length == 0)
                return new List<ProductoSync>();

            const string sql = @"
                SELECT id, id_producto_orig, nom_comercial, nom_comun, descripcion,
                       cant_minima, id_unidad, id_subcategoria, iva_porcentaje,
                       facturable, cod_producto, cant_present, precio_compra,
                       precio_venta, unidad_present, estado_sync, fecha_creacion
                FROM   productos_sync
                WHERE  id_producto_orig = ANY(@ids) AND estado_sync = 'PENDIENTE';";

            var lista = new List<ProductoSync>();
            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("ids", ids);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ProductoSync
                        {
                            Id = reader.GetGuid(0),
                            IdProductoOrig = reader.GetInt32(1),
                            NomComercial = reader.GetString(2),
                            NomComun = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Descripcion = reader.IsDBNull(4) ? null : reader.GetString(4),
                            CantMinima = reader.IsDBNull(5) ? 1m : reader.GetDecimal(5),
                            IdUnidad = reader.IsDBNull(6) ? 1 : reader.GetInt32(6),
                            IdSubcategoria = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                            IvaPorcentaje = reader.IsDBNull(8) ? 0m : reader.GetDecimal(8),
                            Facturable = !reader.IsDBNull(9) && reader.GetBoolean(9),
                            CodProducto = reader.IsDBNull(10) ? null : reader.GetString(10),
                            CantPresent = reader.IsDBNull(11) ? 1m : reader.GetDecimal(11),
                            PrecioCompra = reader.IsDBNull(12) ? 0m : reader.GetDecimal(12),
                            PrecioVenta = reader.IsDBNull(13) ? 0m : reader.GetDecimal(13),
                            UnidadPresent = reader.IsDBNull(14) ? "UN" : reader.GetString(14),
                            EstadoSync = reader.GetString(15),
                            FechaCreacion = reader.GetDateTime(16)
                        });
                    }
                }
            }

            return lista;
        }
 
        /// <summary>
        /// Marca el producto como APLICADO para no re-sincronizarlo.
        /// </summary>
        public static void MarcarAplicado(int idProductoOrig)
        {
            const string sql = @"
                UPDATE productos_sync SET estado_sync = 'APLICADO'
                WHERE  id_producto_orig = @id;";

            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("id", idProductoOrig);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Publica un producto desde Inés María a Supabase para que Matilde lo importe.
        /// Llamar ANTES de enviar la transferencia remota.
        /// </summary>
        public static void PublicarProducto(ProductoSync producto)
        {
            const string sql = @"
                INSERT INTO productos_sync
                    (id_producto_orig, nom_comercial, nom_comun, descripcion,
                     cant_minima, id_unidad, id_subcategoria, iva_porcentaje,
                     facturable, cod_producto, precio_compra, precio_venta, unidad_present)
                VALUES
                    (@id_orig, @nom_c, @nom_u, @desc,
                     @cant, @unidad, @subcat, @iva,
                     @fact, @cod, @pc, @pv, @und)
                ON CONFLICT (id_producto_orig) DO UPDATE
                    SET nom_comercial  = EXCLUDED.nom_comercial,
                        precio_compra  = EXCLUDED.precio_compra,
                        precio_venta   = EXCLUDED.precio_venta,
                        estado_sync    = 'PENDIENTE';";

            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("id_orig", producto.IdProductoOrig);
                cmd.Parameters.AddWithValue("nom_c", producto.NomComercial);
                cmd.Parameters.AddWithValue("nom_u", (object)producto.NomComun ?? DBNull.Value);
                cmd.Parameters.AddWithValue("desc", (object)producto.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("cant", producto.CantMinima);
                cmd.Parameters.AddWithValue("unidad", producto.IdUnidad);
                cmd.Parameters.AddWithValue("subcat", (object)producto.IdSubcategoria ?? DBNull.Value);
                cmd.Parameters.AddWithValue("iva", producto.IvaPorcentaje);
                cmd.Parameters.AddWithValue("fact", producto.Facturable);
                cmd.Parameters.AddWithValue("cod", (object)producto.CodProducto ?? DBNull.Value);
                cmd.Parameters.AddWithValue("pc", producto.PrecioCompra);
                cmd.Parameters.AddWithValue("pv", producto.PrecioVenta);
                cmd.Parameters.AddWithValue("und", (object)producto.UnidadPresent ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
