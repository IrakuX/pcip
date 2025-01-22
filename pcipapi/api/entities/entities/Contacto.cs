using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("contactos")]
    public class Contacto
    {
        [Display(Name = "Activo")]
        [Column("contactoActivo")]
        public bool contactoActivo { get; set; } = false;

        [Key]
        [Display(Name = "Id")]
        [Column("contactoId")]
        public int contactoId { get; set; } = -1;

        [StringLength(500)]
        [Display(Name = "Nombre comercial")]
        [Column("contactoNombreComercial")]
        public string contactoNombreComercial { get; set; } = string.Empty;

        [StringLength(150)]
        [Display(Name = "Nombre")]
        [Column("contactoNombre")]
        public string contactoNombre { get; set; } = string.Empty;

        [StringLength(300)]
        [Display(Name = "Apellidos")]
        [Column("contactoApellidos")]
        public string contactoApellidos { get; set; } = string.Empty;

        [StringLength(150)]
        [Display(Name = "Email")]
        [Column("contactoEmail")]
        public string contactoEmail { get; set; } = string.Empty;

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; } = 19;
    }
}