using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace entities.models.Articulo
{
    public class ArticuloDetalleViewModel : entities.Articulo
    {
        public ArticuloDetalleViewModel()
        {
            this.articuloCodigoNombre = string.Empty;
            this.categoriaArticuloCodigoNombre = string.Empty;
            this.unidadMedidaCodigoNombre = string.Empty;
        }

        [Column("articuloCodigoNombre")]
        [Display(Name = "Articulo código")]
        [JsonPropertyName("articuloCodigoNombre")]
        public string articuloCodigoNombre { get; set; }

        [Column("categoriaArticuloCodigoNombre")]
        [Display(Name = "Categoría código")]
        [JsonPropertyName("categoriaArticuloCodigoNombre")]
        public string categoriaArticuloCodigoNombre { get; set; }

        [Column("unidadMedidaCodigoNombre")]
        [Display(Name = "Unidad medida código")]
        [JsonPropertyName("unidadMedidaCodigoNombre")]
        public string unidadMedidaCodigoNombre { get; set; }
    }
}