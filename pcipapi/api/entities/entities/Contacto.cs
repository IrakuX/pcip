using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("contactos")]
    public class Contacto
    {
        public Contacto()
        {
            this.contactoId = -1;
            this.contactoNombreComercial = string.Empty;
            this.contactoNombre = string.Empty;
            this.contactoApellidos = string.Empty;
            this.contactoEmail = string.Empty;
            this.contactoActivo = false;
            this.objetoId = 19;
        }

        [Display(Name = "Activo")]
        [Column("contactoActivo")]
        public bool contactoActivo { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("contactoId")]
        public int contactoId { get; set; }

        [StringLength(500)]
        [Display(Name = "Nombre comercial")]
        [Column("contactoNombreComercial")]
        public string contactoNombreComercial { get; set; }

        [StringLength(150)]
        [Display(Name = "Nombre")]
        [Column("contactoNombre")]
        public string contactoNombre { get; set; }

        [StringLength(300)]
        [Display(Name = "Apellidos")]
        [Column("contactoApellidos")]
        public string contactoApellidos { get; set; }

        [StringLength(150)]
        [Display(Name = "Email")]
        [Column("contactoEmail")]
        public string contactoEmail { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }
    }
}