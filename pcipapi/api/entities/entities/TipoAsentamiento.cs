using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("tiposAsentamientos")]
    public class TipoAsentamiento
    {
        public TipoAsentamiento()
        {
            this.tipoAsentamientoId = -1;
            this.tipoAsentamientoNombre = string.Empty;
            this.tipoAsentamientoActivo = false;
            this.objetoId = 2;
        }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("tipoAsentamientoId")]
        public int tipoAsentamientoId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("tipoAsentamientoNombre")]
        public string tipoAsentamientoNombre { get; set; }

        [Display(Name = "Activo")]
        [Column("tipoAsentamientoActivo")]
        public bool tipoAsentamientoActivo { get; set; }
    }
}