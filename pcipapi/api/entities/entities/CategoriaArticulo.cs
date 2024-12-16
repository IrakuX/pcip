using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("categoriasArticulo")]
    public class CategoriaArticulo
    {
        public CategoriaArticulo()
        {
            this.categoriaArticuloId = -1;
            this.categoriaArticuloNombre = string.Empty;
            this.categoriaArticuloActivo = false;
            this.objetoId = 10;
        }

        [Display(Name = "Activo")]
        [Column("categoriaArticuloActivo")]
        public bool categoriaArticuloActivo { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("categoriaArticuloId")]
        public int categoriaArticuloId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("categoriaArticuloNombre")]
        public string categoriaArticuloNombre { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }
    }
}