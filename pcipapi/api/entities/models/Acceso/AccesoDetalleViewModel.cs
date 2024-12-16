using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace entities.models.Acceso
{
    public class AccesoDetalleViewModel : entities.Acceso
    {
        [Column("objetoNombre")]
        [Display(Name = "Objeto nombre")]
        [JsonPropertyName("objetoNombre")]
        public string objetoNombre { get; set; } = string.Empty;
    }
}