using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("accesos")]
    public class Acceso
    {
        public Acceso()
        {
            this.perfilId = 0;
            this.objetoId = 0;
            this.accesoActivo = false;
        }

        [Column("accesoActivo")]
        [Display(Name = "Activo")]
        public bool accesoActivo { get; set; }

        [Key]
        [Required]
        [Column("objetoId")]
        [Display(Name = "Objeto")]
        public int objetoId { get; set; }

        [Key]
        [Required]
        [Column("perfilId")]
        [Display(Name = "Perfil")]
        public int perfilId { get; set; }
    }
}