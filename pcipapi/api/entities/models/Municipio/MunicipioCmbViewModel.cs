using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace entities.models.Municipio
{
    public class MunicipioCmbViewModel : entities.Municipio
    {
        [Display(Name = "estadoNombre")]
        [Column("estadoNombre")]
        public string estadoNombre { get; set; } = string.Empty;
    }
}