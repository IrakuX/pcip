using Newtonsoft.Json;

using System.Text.Json.Serialization;

namespace entities.models
{
    public class ErrorModel
    {
        private static readonly Dictionary<int, string> ErrorDetalles = new()
        {
            { 101, "Registro está siendo utilizado en otra área del sistema." },
            { 106, "Registro de empleado tiene o puede tener relación con : APIARIOS, USUARIOS, EQUIPOS DE TRABAJO, RUTAS, ENTRADA DE ALMACÉN, SALIDA DE ALMACÉN." }
        };

        [JsonProperty("error")]
        [JsonPropertyName("error")]
        public object? error { get; set; }

        [JsonProperty("errorCodigo")]
        [JsonPropertyName("errorCodigo")]
        public int errorCodigo { get; set; } = 0;

        [JsonProperty("errorDetalle")]
        [JsonPropertyName("errorDetalle")]
        public string errorDetalle => ErrorDetalles.TryGetValue(errorCodigo, out var detalle) ? detalle : string.Empty;

        [JsonProperty("errorMensaje")]
        [JsonPropertyName("errorMensaje")]
        public string errorMensaje { get; set; } = string.Empty;
    }
}