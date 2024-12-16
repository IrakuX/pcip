using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("articulos")]
    public class Articulo
    {
        public Articulo()
        {
            this.articuloId = 0;
            this.articuloCodigo = string.Empty;
            this.articuloNombre = string.Empty;
            this.articuloDescripcion = string.Empty;
            this.articuloOrigen = 0;
            this.articuloCantidadMinima = 0;
            this.articuloCantidadMaxima = 0;
            this.articuloCompuesto = false;
            this.categoriaArticuloId = null;
            this.unidadMedidaId = null;
            this.articuloActivo = false;
            this.objetoId = 18;
        }

        [Display(Name = "Activo")]
        [Column("articuloActivo")]
        public bool articuloActivo { get; set; }

        [Display(Name = "Cantidad máxima")]
        [Column("articuloCantidadMaxima")]
        public int articuloCantidadMaxima { get; set; }

        [Display(Name = "Cantidad mínima")]
        [Column("articuloCantidadMinima")]
        public int articuloCantidadMinima { get; set; }

        [StringLength(10)]
        [Display(Name = "Código")]
        [Column("articuloCodigo")]
        public string articuloCodigo { get; set; }

        [Display(Name = "Compuesto")]
        [Column("articuloCompuesto")]
        public bool articuloCompuesto { get; set; }

        [StringLength(500)]
        [Display(Name = "Descripción")]
        [Column("articuloDescripcion")]
        public string articuloDescripcion { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("articuloId")]
        public int articuloId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("articuloNombre")]
        public string articuloNombre { get; set; }

        [Display(Name = "Origen")]
        [Column("articuloOrigen")]
        public int articuloOrigen { get; set; }

        [ForeignKey("categoriaArticuloId")]
        [Display(Name = "Categoría articulo")]
        [Column("categoriaArticuloId")]
        public int? categoriaArticuloId { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }

        [ForeignKey("unidadMedidaId")]
        [Display(Name = "Unidad de medida")]
        [Column("unidadMedidaId")]
        public int? unidadMedidaId { get; set; }
    }
}