using Newtonsoft.Json;
using Npgsql;
using SupabaseDataAccess.Models;
using System;
using System.Collections.Generic;

namespace SupabaseDataAccess.Repositories
{
    public static class TransferenciaRepository
    {
        // ── Ya existía: SubirTransferencia, ObtenerPendientesPorDestino, etc. ──

        /// <summary>
        /// Retorna lista tipada de transferencias PENDIENTES para una bodega.
        /// </summary>
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
                cmd.Parameters.AddWithValue("dest", idBodegaDestino);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var t = new Transferencia
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
                        };
                        lista.Add(t);
                    }
                }
            }
            return lista;
        }
    }

}
