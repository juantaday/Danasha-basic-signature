using CADsisVenta.Funtions;
using DomainSQLite.Setting;
using Newtonsoft.Json;
using SupabaseDataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace SupabaseWorker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Redirigir consola a archivo de log
            string logPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "worker-log.txt");

            // Limpiar log si supera cierto tamaño para evitar archivos gigantes
            LimpiarLog(logPath, maxLineas: 500);

            Console.WriteLine($"=== SupabaseWorker iniciado: {DateTime.Now:dd/MM/yyyy HH:mm:ss} ===");

            using (var log = new StreamWriter(logPath, append: true))
            {
                log.AutoFlush = true;
                Console.SetOut(log);

                try
                {
                    Console.WriteLine($"\n=== SupabaseWorker iniciado: {DateTime.Now:dd/MM/yyyy HH:mm:ss} ===");

                    // Inicializar conexión igual que la app principal
                    InicializarConexion();

                    List<SupabasePendienteDto> pendientes = ObtenerPendientes();

                    if (pendientes.Count == 0)
                    {
                        Console.WriteLine("Sin pendientes. Saliendo.");
                        return;
                    }

                    Console.WriteLine($"Procesando {pendientes.Count} pendiente(s)...");

                    foreach (var p in pendientes)
                    {
                        try
                        {
                            object detalle = null;
                            if (!string.IsNullOrEmpty(p.DetalleJson))
                                detalle = JsonConvert.DeserializeObject(p.DetalleJson);

                            bool enviado = TransferenciaRepository.ActualizarEstado(
                                p.SupabaseId, p.Estado, p.Novedad, detalle);

                            if (enviado)
                            {
                                MarcarEnviado(p.Id);
                                Console.WriteLine($"  ✓ {p.SupabaseId} → ENVIADO");
                            }
                            else
                            {
                                RegistrarFallo(p.Id);
                                Console.WriteLine($"  ✗ {p.SupabaseId} → sin respuesta de Supabase");
                            }
                        }
                        catch (Exception ex)
                        {
                            RegistrarFallo(p.Id);
                            Console.WriteLine($"  ✗ {p.SupabaseId} → ERROR: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR CRÍTICO: {ex.Message}");
                }

                Console.WriteLine("=== Worker terminado ===");

            }
               
        }

        // ── Inicializar conexión desde el archivo encriptado ──────────────────────
        private static void InicializarConexion()
        {
            // Reutiliza exactamente la misma lógica que la app principal
            var cnn = DomainSQLite.Funtions.FunctionSQLite
                          .GetDefaultConectionInLine()
                          .GetAwaiter()
                          .GetResult();

            if (cnn.Id != 1)
                throw new Exception("No se pudo leer la configuración de conexión.");

            Configuration.ConectionString = string.Format(
                "Data Source={0};Initial Catalog={1};" +
                "Persist Security Info=True;User ID={2};Password={3};",
                cnn.IpConection,
                cnn.NameDatabase,
                cnn.UserId,
                cnn.Password);

            Console.WriteLine($"  Conectado a: {cnn.IpConection} / {cnn.NameDatabase}");
        }

        // ── BD local ─────────────────────────────────────────────────────────────
        private static List<SupabasePendienteDto> ObtenerPendientes(string modulo = "TRANSFERENCIA")
        {
            var lista = new List<SupabasePendienteDto>();

            using (var cmd = new SqlComandExec())
            {
                DataTable dt = cmd.RetornaTablaConParams(
                    "SELECT Id, SupabaseId, Estado, Novedad, DetalleJson, Intentos " +
                    "FROM SupabasePendientes " +
                    "WHERE EstadoEnvio = 'PENDIENTE' AND Modulo = @mod " +
                    "ORDER BY FechaCreacion",
                    new[] { "@mod" },
                    new object[] { modulo });

                foreach (DataRow row in dt.Rows)
                {
                    lista.Add(new SupabasePendienteDto
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        SupabaseId = row["SupabaseId"].ToString(),
                        Estado = row["Estado"].ToString(),
                        Novedad = row["Novedad"] == DBNull.Value ? null : row["Novedad"].ToString(),
                        DetalleJson = row["DetalleJson"] == DBNull.Value ? null : row["DetalleJson"].ToString(),
                        Intentos = Convert.ToInt32(row["Intentos"])
                    });
                }
            }

            return lista;
        }

        private static void MarcarEnviado(int id)
        {
            using (var cmd = new SqlComandExec())
            {
                cmd.EjecutarConParams(
                    "UPDATE SupabasePendientes SET EstadoEnvio='ENVIADO', UltimoIntento=GETDATE() WHERE Id=@id",
                    new[] { "@id" }, new object[] { id });
            }
        }

        private static void RegistrarFallo(int id)
        {
            using (var cmd = new SqlComandExec())
            {
                cmd.EjecutarConParams(
                    "UPDATE SupabasePendientes SET Intentos=Intentos+1, UltimoIntento=GETDATE() WHERE Id=@id",
                    new[] { "@id" }, new object[] { id });
            }
        }

        private static void LimpiarLog(string logPath,int maxLineas = 500)
        {
            try
            {
               

                if (!File.Exists(logPath)) return;

                string[] lineas = File.ReadAllLines(logPath);

                // Solo limpia si supera el límite
                if (lineas.Length <= maxLineas) return;

                // Conserva solo las últimas N líneas
                string[] ultimas = lineas.Skip(lineas.Length - maxLineas).ToArray();
                File.WriteAllLines(logPath, ultimas);
            }
            catch
            {
                // Si falla el log, no interrumpir el worker
            }
        }
    }

    internal class SupabasePendienteDto
    {
        public int Id { get; set; }
        public string SupabaseId { get; set; }
        public string Estado { get; set; }
        public string Novedad { get; set; }
        public string DetalleJson { get; set; }
        public int Intentos { get; set; }
    }
}