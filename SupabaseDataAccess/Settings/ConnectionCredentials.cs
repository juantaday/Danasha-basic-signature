using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace SupabaseDataAccess.Settings
{
    public static class ConnectionCredentials
    {
        // Carpeta: C:\Users\TuUsuario\AppData\Roaming\TuApp\
        private static string FilePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "DanashaBasicSignature", 
            "conn.dat"
        );

        // ── Guardar ──────────────────────────────────────────

        public static bool SaveCredentials(string host, string database,
                                   string username, string password)
        {

            try
            {
                var data = new
                {
                    Host = host,
                    Database = database,
                    Username = username,
                    Password = password
                };

                string json = JsonConvert.SerializeObject(data);
                byte[] encrypted = ProtectedData.Protect(
                    Encoding.UTF8.GetBytes(json), null,
                    DataProtectionScope.CurrentUser);

                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
                File.WriteAllBytes(FilePath, encrypted);
                return true;  
            }
            catch
            {
                return false;  
            }
        }

        public static string GetConnectionString(bool usePool = true)
        {
            if (!File.Exists(FilePath))
                throw new FileNotFoundException("No hay credenciales guardadas.");

            byte[] encrypted = File.ReadAllBytes(FilePath);
            byte[] decrypted = ProtectedData.Unprotect(encrypted, null,
                                                        DataProtectionScope.CurrentUser);

            string json = Encoding.UTF8.GetString(decrypted);
            dynamic data = JsonConvert.DeserializeObject(json);

            // Puerto y configuración cambian según el modo
            string port = usePool ? "6543" : "5432";
            string pooling = usePool ? "true" : "false";

            return $"Host={data.Host};" +
                   $"Port={port};" +
                   $"Database={data.Database};" +
                   $"Username={data.Username};" +
                   $"Password={data.Password};" +
                   "SSL Mode=Require;" +
                   "Trust Server Certificate=true;" +
                   $"Pooling={pooling};" +
                   "Timeout=30;";
        }


        public static bool CredentialsExist() => File.Exists(FilePath);

        
        // Método para cargar y retornar las credenciales desencriptadas
        public static CredentialData LoadCredentials()
        {
            if (!File.Exists(FilePath))
                throw new FileNotFoundException("No hay credenciales guardadas.");

            byte[] encrypted = File.ReadAllBytes(FilePath);
            byte[] decrypted = ProtectedData.Unprotect(
                encrypted, null, DataProtectionScope.CurrentUser);

            string json = Encoding.UTF8.GetString(decrypted);
            return JsonConvert.DeserializeObject<CredentialData>(json);
        }

        public static (string Host, string Database, string Username, string Password) LoadCredentialsTuple()
        {
            var data = LoadCredentials();
            return (data.Host, data.Database, data.Username, data.Password);
        }

    }

    public class CredentialData
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
