using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace entities.models
{
    public class ResponseModel<T>
    {
        [Column("data")]
        [Display(Name = "Data")]
        [JsonPropertyName("data")]
        public T? data { get; set; }

        [Column("error")]
        [Display(Name = "Error")]
        [JsonPropertyName("error")]
        public ErrorModel? error { get; set; } = null;

        [Column("resultado")]
        [Display(Name = "Resultado")]
        [JsonPropertyName("resultado")]
        public bool resultado { get; set; } = false;
    }
}