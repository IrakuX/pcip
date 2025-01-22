using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace entities.models.Asentamiento
{
    public class AsentamientoCmbViewModel : entities.Asentamiento
    {
        [Display(Name = "Ciudad nombre")]
        [Column("ciudadNombre")]
        public string ciudadNombre { get; set; } = string.Empty;

        [Display(Name = "Estado nombre")]
        [Column("estadoNombre")]
        public string estadoNombre { get; set; } = string.Empty;

        [Display(Name = "Municipio nombre")]
        [Column("municipioNombre")]
        public string municipioNombre { get; set; } = string.Empty;

        [Display(Name = "Tipo de asentamiento nombre")]
        [Column("tipoAsentamientoId")]
        public string tipoAsentamientoNombre { get; set; } = string.Empty;
    }
}