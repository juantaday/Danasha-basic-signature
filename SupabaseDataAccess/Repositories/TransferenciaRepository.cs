using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using Npgsql;
using SupabaseDataAccess.Models;

namespace SupabaseDataAccess.Repositories
{
    public static class TransferenciaRepository
    {
        // ── 1. Insertar ──────────────────────────────────────────────────────
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

        // ── 2. Pendientes como DataTable (para DataGridView directo) ─────────
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

        // ── 3. Pendientes como objetos tipados (para lógica en código) ───────
        public static List<Transferencia> ObtenerPendientesComoObjetos(int idBodegaDestino)
        {
            const string sql = @"
                SELECT id, num_transferencia, bodega_origen_id, bodega_origen_nom,
                       bodega_destino_id, bodega_destino_nom, fecha_emision,
                       estado, novedad, fecha_recepcion, detalle::text
                FROM   transferencias
                WHERE  bodega_destino_id = @dest AND estado = 'PENDIENTE'
                ORDER  BY fecha_emision DESC;";

            var lista = new List<Transferencia>();
            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.CommandTimeout = 10;
                cmd.Parameters.AddWithValue("dest", idBodegaDestino);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Transferencia
                        {
                            Id = reader.GetGuid(0),
                            NumTransferencia = reader.GetString(1),
                            BodegaOrigenId = reader.GetInt32(2),
                            BodegaOrigenNom = reader.GetString(3),
                            BodegaDestinoId = reader.GetInt32(4),
                            BodegaDestinoNom = reader.GetString(5),
                            FechaEmision = reader.GetDateTime(6),
                            Estado = reader.GetString(7),
                            Novedad = reader.IsDBNull(8) ? null : reader.GetString(8),
                            FechaRecepcion = reader.IsDBNull(9) ? (DateTime?)null : reader.GetDateTime(9),
                            Detalle = JsonConvert.DeserializeObject<List<DetalleTransferencia>>(
                                                   reader.GetString(10))
                        });
                    }
                }
            }
            return lista;
        }

        // ── 4. Actualizar estado ─────────────────────────────────────────────
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
                cmd.CommandTimeout = 10;
                cmd.Parameters.AddWithValue("@id", supabaseId);
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@novedad",
                    (object)novedad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@detalle",
                    detalleActualizado != null
                        ? (object)JsonConvert.SerializeObject(detalleActualizado)
                        : DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        // ── 5. Historial ─────────────────────────────────────────────────────
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