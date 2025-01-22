using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace entities.models.Almacen
{
    public class AlmacenViewModel : entities.Almacen
    {
        [Display(Name = "objetoNombre")]
        [Column("objetoNombre")]
        public string objetoNombre { get; set; } = string.Empty;
    }
}