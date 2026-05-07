using System;
using System.Data;
using Newtonsoft.Json;
using Npgsql;

namespace SupabaseDataAccess
{
    /// <summary>
    /// CRUD sobre la tabla 'transferencias' en Supabase PostgreSQL.
    /// Métodos síncronos para compatibilidad con WinForms .NET Framework.
    /// </summary>
    public static class TransferenciaRepository
    {
        /// <summary>
        /// Inserta una transferencia. Devuelve el UUID generado o null si falla.
        /// </summary>
        public static string SubirTransferencia(
            string numTransferencia,
            int bodegaOrigenId, string bodegaOrigenNom,
            int bodegaDestinoId, string bodegaDestinoNom,
            object detalle)
        {
            const string sql = @"
                INSERT INTO transferencias
                    (num_transferencia,
                     bodega_origen_id,  bodega_origen_nom,
                     bodega_destino_id, bodega_destino_nom,
                     detalle)
                VALUES (@num, @orig_id, @orig_nom, @dest_id, @dest_nom, @detalle::jsonb)
                RETURNING id;";

            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@num", numTransferencia);
                cmd.Parameters.AddWithValue("@orig_id", bodegaOrigenId);
                cmd.Parameters.AddWithValue("@orig_nom", bodegaOrigenNom);
                cmd.Parameters.AddWithValue("@dest_id", bodegaDestinoId);
                cmd.Parameters.AddWithValue("@dest_nom", bodegaDestinoNom);
                cmd.Parameters.AddWithValue("@detalle", JsonConvert.SerializeObject(detalle));
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        /// <summary>
        /// Transferencias PENDIENTES para una bodega destino.
        /// Columnas: id, num_transferencia, bodega_origen_nom, fecha_emision, estado, detalle
        /// </summary>
        public static DataTable ObtenerPendientesPorDestino(int idBodegaDestino)
        {
            const string sql = @"
                SELECT id, num_transferencia, bodega_origen_nom,
                       fecha_emision, estado, detalle::text AS detalle
                FROM   transferencias
                WHERE  bodega_destino_id = @dest_id AND estado = 'PENDIENTE'
                ORDER  BY fecha_emision DESC;";

            var dt = new DataTable();
            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@dest_id", idBodegaDestino);
                using (var adapter = new NpgsqlDataAdapter(cmd))
                    adapter.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// Actualiza estado a RECIBIDO o CON_NOVEDAD.
        /// Opcionalmente actualiza el JSONB de detalle con cantidades reales recibidas.
        /// </summary>
        public static bool ActualizarEstado(
            string supabaseId, string estado,
            string novedad = null, object detalleActualizado = null)
        {
            const string sql = @"
                UPDATE transferencias
                SET    estado          = @estado,
                       novedad         = @novedad,
                       fecha_recepcion = NOW(),
                       detalle         = COALESCE(@detalle::jsonb, detalle)
                WHERE  id = @id::uuid;";

            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", supabaseId);
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@novedad", (object)novedad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@detalle",
                    detalleActualizado != null
                        ? (object)JsonConvert.SerializeObject(detalleActualizado)
                        : DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Historial de transferencias de los últimos N días (default 30).
        /// </summary>
        public static DataTable ObtenerHistorial(int ultimosDias = 30)
        {
            const string sql = @"
                SELECT id, num_transferencia, bodega_origen_nom, bodega_destino_nom,
                       fecha_emision, estado, novedad, fecha_recepcion
                FROM   transferencias
                WHERE  fecha_emision >= NOW() - (@dias || ' days')::interval
                ORDER  BY fecha_emision DESC;";

            var dt = new DataTable();
            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@dias", ultimosDias);
                using (var adapter = new NpgsqlDataAdapter(cmd))
                    adapter.Fill(dt);
            }
            return dt;
        }
    }
}
