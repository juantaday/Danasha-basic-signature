using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CADsisVenta.Helpers
{
    public static class SupabaseHelper
    {
        private static readonly string _url = ConfigurationManager.AppSettings["SupabaseUrl"];
        private static readonly string _key = ConfigurationManager.AppSettings["SupabaseApiKey"];
        private static readonly object _fileLock = new object();

        private static bool UseSimulated => false;

        private static string StoragePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "DanashaBasicSignature",
            "supabase_transferencias.json");

        private static HttpClient GetClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("apikey", _key);
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _key);
            return client;
        }

        private static JArray LoadStore()
        {
            if (!File.Exists(StoragePath))
                return new JArray();
            var json = File.ReadAllText(StoragePath);
            return string.IsNullOrWhiteSpace(json) ? new JArray() : JArray.Parse(json);
        }

        private static void SaveStore(JArray data)
        {
            var dir = Path.GetDirectoryName(StoragePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(StoragePath, data.ToString(Formatting.Indented));
        }

        /// <summary>
        /// Sube una transferencia a Supabase. Devuelve el UUID generado o null si falla.
        /// </summary>
        public static async Task<string> SubirTransferenciaAsync(object payload)
        {
            using (var client = GetClient())
            {
                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Header Prefer va en el request, no en DefaultRequestHeaders
                // para evitar excepción si se llama más de una vez
                var request = new HttpRequestMessage(HttpMethod.Post,
                    $"{_url}/rest/v1/transferencias");
                request.Content = content;
                request.Headers.Add("Prefer", "return=representation");

                var resp = await client.SendAsync(request);
                if (!resp.IsSuccessStatusCode) return null;

                var body = await resp.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject<dynamic>(body);
                return result[0]?.id?.ToString();
            }
        }

        /// <summary>
        /// Obtiene transferencias PENDIENTES para una bodega destino.
        /// </summary>
        public static async Task<string> ObtenerTransferenciasPendientesAsync(int idBodegaDestino)
        {
            using (var client = GetClient())
            {
                var url = $"{_url}/rest/v1/transferencias" +
                          $"?bodega_destino_id=eq.{idBodegaDestino}&estado=eq.PENDIENTE" +
                          "&order=fecha_emision.desc";
                var resp = await client.GetAsync(url);
                if (!resp.IsSuccessStatusCode) return null;
                return await resp.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// Actualiza el estado de una transferencia en Supabase (RECIBIDO o CON_NOVEDAD).
        /// </summary>
        public static async Task<bool> ActualizarEstadoAsync(string supabaseId, string estado,
                                                              string novedad = null)
        {
            using (var client = GetClient())
            {
                var payload = new { estado, novedad, fecha_recepcion = DateTime.UtcNow };
                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // PatchAsync no existe en .NET Framework 4.8 — usar SendAsync
                var request = new HttpRequestMessage(new HttpMethod("PATCH"),
                    $"{_url}/rest/v1/transferencias?id=eq.{supabaseId}");
                request.Content = content;

                var resp = await client.SendAsync(request);
                return resp.IsSuccessStatusCode;
            }
        }
    }
}