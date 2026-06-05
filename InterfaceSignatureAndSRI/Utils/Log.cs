using System;
using System.IO;
using DomainSQLite.Helpers;

namespace InterfaceSignatureAndSRI.Utils
{
    public enum LogLevel
    {
        WARNING,
        ERROR
    }

    public static class Log
    {
        // ── Escritura pública ────────────────────────────────────────────────

        public static void Warning(string context, string message)
            => Write(LogLevel.WARNING, context, message, null);

        public static void Error(string context, string message, Exception ex = null)
            => Write(LogLevel.ERROR, context, message, ex);

        // ── Núcleo ───────────────────────────────────────────────────────────
        private static void Write(LogLevel level, string context, string message, Exception ex)
        {
            try
            {
                string folder = AppSetting.GetDefaultFolderLogs();
                string fileName = $"log_{DateTime.Now:yyyy_MM_dd}.txt";
                string filePath = Path.Combine(folder, fileName);

                var sb = new System.Text.StringBuilder();
                sb.AppendLine($"[{DateTime.Now:HH:mm:ss}] [{level}] [{context}]");
                sb.AppendLine($"  {message}");

                if (ex != null)
                {
                    sb.AppendLine($"  Exception : {ex.GetType().Name}: {ex.Message}");
                    if (ex.InnerException != null)
                        sb.AppendLine($"  Inner     : {ex.InnerException.Message}");
                    sb.AppendLine($"  StackTrace: {ex.StackTrace}");
                }

                sb.AppendLine(new string('-', 72));

                // ── Leer contenido existente y prepend ──────────────────────────
                string contenidoExistente = File.Exists(filePath)
                    ? File.ReadAllText(filePath)
                    : string.Empty;

                File.WriteAllText(filePath, sb.ToString() + contenidoExistente);
            }
            catch
            {
                // El log nunca debe romper el flujo
            }
        }
    }
}