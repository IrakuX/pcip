using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace entities.models.Acceso
{
    public class AccesoViewModel : entities.Acceso
    {
        [Column("accesoDetalle")]
        [Display(Name = "Detalle")]
        [JsonPropertyName("accesoDetalle")]
        public IReadOnlyList<AccesoDetalleViewModel?>? accesoDetalle { get; set; }

        [Column("perfilNombre")]
        [Display(Name = "Perfil nombre")]
        [JsonPropertyName("perfilNombre")]
        public string perfilNombre { get; set; } = string.Empty;
    }
}