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

        private static bool UseSimulated =>
            string.IsNullOrWhiteSpace(_url) || string.IsNullOrWhiteSpace(_key);

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
            if (!UseSimulated)
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

            // Modo simulado: guarda en archivo JSON local
            return await Task.Run(() =>
            {
                lock (_fileLock)
                {
                    var store = LoadStore();
                    var payloadJson = JObject.FromObject(payload ?? new { });
                    var id = Guid.NewGuid().ToString();

                    var item = new JObject
                    {
                        ["id"] = id,
                        ["num_transferencia"] = payloadJson["num_transferencia"],
                        ["bodega_origen_id"] = payloadJson["bodega_origen_id"],
                        ["bodega_origen_nom"] = payloadJson["bodega_origen_nom"],
                        ["bodega_destino_id"] = payloadJson["bodega_destino_id"],
                        ["bodega_destino_nom"] = payloadJson["bodega_destino_nom"],
                        ["fecha_emision"] = DateTime.UtcNow,
                        ["estado"] = "PENDIENTE",
                        ["novedad"] = null,
                        ["fecha_recepcion"] = null,
                        ["detalle"] = payloadJson["detalle"] ?? new JArray()
                    };

                    store.Add(item);
                    SaveStore(store);
                    return id;
                }
            });
        }

        /// <summary>
        /// Obtiene transferencias PENDIENTES para una bodega destino.
        /// </summary>
        public static async Task<string> ObtenerTransferenciasPendientesAsync(int idBodegaDestino)
        {
            if (!UseSimulated)
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

            // Modo simulado: lee y filtra el archivo local
            return await Task.Run(() =>
            {
                lock (_fileLock)
                {
                    var store = LoadStore();
                    var filtered = new JArray(store.Where(item =>
                        string.Equals(item["estado"]?.ToString(), "PENDIENTE",
                            StringComparison.OrdinalIgnoreCase) &&
                        item["bodega_destino_id"] != null &&
                        item["bodega_destino_id"].Value<int>() == idBodegaDestino));

                    return filtered.ToString(Formatting.None);
                }
            });
        }

        /// <summary>
        /// Actualiza el estado de una transferencia en Supabase (RECIBIDO o CON_NOVEDAD).
        /// </summary>
        public static async Task<bool> ActualizarEstadoAsync(string supabaseId, string estado,
                                                              string novedad = null)
        {
            if (!UseSimulated)
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

            // Modo simulado: actualiza el archivo local
            return await Task.Run(() =>
            {
                lock (_fileLock)
                {
                    var store = LoadStore();
                    var item = store.FirstOrDefault(x =>
                        string.Equals(x["id"]?.ToString(), supabaseId,
                            StringComparison.OrdinalIgnoreCase));
                    if (item == null) return false;

                    item["estado"] = estado;
                    item["novedad"] = string.IsNullOrWhiteSpace(novedad) ? null : novedad;
                    item["fecha_recepcion"] = DateTime.UtcNow;
                    SaveStore(store);
                    return true;
                }
            });
        }
    }
}