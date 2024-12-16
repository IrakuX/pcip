using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("objetoTipos")]
    public class ObjetoTipo
    {
        [Display(Name = "Activo")]
        [Column("objetoTipoActivo")]
        public bool objetoTipoActivo { get; set; } = false;

        [Key]
        [Required]
        [Display(Name = "Id")]
        [Column("objetoTipoId")]
        public int objetoTipoId { get; set; } = -1;

        [StringLength(50)]
        [Display(Name = "Nombre")]
        [Column("objetoTipoNombre")]
        public string objetoTipoNombre { get; set; } = string.Empty;
    }
}