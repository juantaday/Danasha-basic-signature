using Npgsql;
using System.Collections.Generic;

namespace SupabaseDataAccess.Migrations
{
    public static class SupabaseMigrations
    {
        public static void RunMigrations()
        {
            using (var conn = SupabasePgConnection.OpenDirectConnection())
            {
                CrearTablaMigraciones(conn);

                var migrations = new Dictionary<string, string>
                {
                    ["001_crear_transferencias"] = Migration_001_Transferencias(),
                    ["002_crear_productos_sync"] = Migration_002_ProductosSync(),
                    ["003_index_transferencias"] = Migration_003_Indices(),
                };

                foreach (var m in migrations)
                {
                    if (!MigracionYaEjecutada(conn, m.Key))
                        EjecutarMigracion(conn, m.Key, m.Value);
                }

            }
        }

        // ── Scripts ──────────────────────────────────────────────────────────

        private static string Migration_001_Transferencias() => @"
        CREATE TABLE IF NOT EXISTS transferencias (
            id                  UUID         DEFAULT gen_random_uuid() PRIMARY KEY,
            num_transferencia   TEXT         NOT NULL,
            bodega_origen_id    INT          NOT NULL,
            bodega_origen_nom   TEXT         NOT NULL,
            bodega_destino_id   INT          NOT NULL,
            bodega_destino_nom  TEXT         NOT NULL,
            fecha_emision       TIMESTAMPTZ  NOT NULL DEFAULT now(),
            estado              TEXT         NOT NULL DEFAULT 'PENDIENTE',
            novedad             TEXT,
            fecha_recepcion     TIMESTAMPTZ,
            detalle             JSONB        NOT NULL DEFAULT '[]'::jsonb
        );";

        private static string Migration_002_ProductosSync() => @"
        CREATE TABLE IF NOT EXISTS productos_sync (
            id               UUID          DEFAULT gen_random_uuid() PRIMARY KEY,
            id_producto_orig INT           NOT NULL,
            nom_comercial    TEXT          NOT NULL,
            nom_comun        TEXT,
            descripcion      TEXT,
            cant_minima      DECIMAL(18,4) DEFAULT 1,
            id_unidad        INT           DEFAULT 1,
            id_subcategoria  INT,
            iva_porcentaje   DECIMAL(5,2)  DEFAULT 0,
            facturable       BOOLEAN       DEFAULT TRUE,
            cod_producto     TEXT,
            cant_present     DECIMAL(18,4) DEFAULT 1,
            precio_compra    DECIMAL(18,6) DEFAULT 0,
            precio_venta     DECIMAL(18,6) DEFAULT 0,
            unidad_present   TEXT,
            estado_sync      TEXT          DEFAULT 'PENDIENTE',
            fecha_creacion   TIMESTAMPTZ   DEFAULT now()
        );";

        private static string Migration_003_Indices() => @"
        CREATE INDEX IF NOT EXISTS idx_transferencias_destino
            ON transferencias(bodega_destino_id, estado);

        CREATE INDEX IF NOT EXISTS idx_productos_sync_estado
            ON productos_sync(estado_sync);";

        // ── Control de migraciones ────────────────────────────────────────────

        private static void CrearTablaMigraciones(NpgsqlConnection conn)
        {
            var sql = @"CREATE TABLE IF NOT EXISTS _migraciones (
                        id           BIGSERIAL PRIMARY KEY,
                        nombre       TEXT UNIQUE NOT NULL,
                        ejecutada_en TIMESTAMPTZ DEFAULT NOW()
                    );";
            using (var cmd = new NpgsqlCommand(sql, conn))
                cmd.ExecuteNonQuery();
        }

        private static bool MigracionYaEjecutada(NpgsqlConnection conn, string nombre)
        {
            using (var cmd = new NpgsqlCommand(
                "SELECT COUNT(1) FROM _migraciones WHERE nombre = @n", conn))
            {
                cmd.Parameters.AddWithValue("n", nombre);
                return (long)cmd.ExecuteScalar() > 0;
            }
        }

        private static void EjecutarMigracion(NpgsqlConnection conn, string nombre, string sql)
        {
            using (var cmd = new NpgsqlCommand(sql, conn))
                cmd.ExecuteNonQuery();

            using (var cmd = new NpgsqlCommand(
                "INSERT INTO _migraciones (nombre) VALUES (@n)", conn))
            {
                cmd.Parameters.AddWithValue("n", nombre);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
