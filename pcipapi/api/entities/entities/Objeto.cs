using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("articulos")]
    public class Objeto
    {
        public Objeto()
        {
            this.objetoId = -1;
            this.objetoNombre = string.Empty;
            this.objetoTipoId = null;
            this.menuId = null;
            this.objetoActivo = false;
        }

        [Display(Name = "Menu")]
        [Column("menuId")]
        public int? menuId { get; set; }

        [Display(Name = "Activo")]
        [Column("objetoActivo")]
        public bool objetoActivo { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("objetoId")]
        public int objetoId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("objetoNombre")]
        public string objetoNombre { get; set; }

        [Display(Name = "Objeto tipo")]
        [Column("objetoTipoId")]
        public int? objetoTipoId { get; set; }
    }
}