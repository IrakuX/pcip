using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("fraccionesArancelarias")]
    public class FraccionArancelaria
    {
        public FraccionArancelaria()
        {
            this.fraccionArancelariaId = -1;
            this.fraccionArancelariaNombre = string.Empty;
            this.fraccionArancelariaFechaInicioVigencia = null;
            this.fraccionArancelariaFechaFinVigencia = null;
            this.unidadId = null;
            this.fraccionArancelariaActivo = false;
            this.objetoId = 12;
        }

        [Display(Name = "Activo")]
        [Column("fraccionArancelariaActivo")]
        public bool fraccionArancelariaActivo { get; set; }

        [Display(Name = "Fecha fin vigencia")]
        [Column("fraccionArancelariaFechaFinVigencia")]
        public DateTime? fraccionArancelariaFechaFinVigencia { get; set; }

        [Display(Name = "Fecha inicio vigencia")]
        [Column("fraccionArancelariaFechaInicioVigencia")]
        public DateTime? fraccionArancelariaFechaInicioVigencia { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("fraccionArancelariaId")]
        public int fraccionArancelariaId { get; set; }

        [StringLength(500)]
        [Display(Name = "Nombre")]
        [Column("fraccionArancelariaNombre")]
        public string fraccionArancelariaNombre { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }

        [ForeignKey("unidadId")]
        [Display(Name = "Unidad")]
        [Column("unidadId")]
        public int? unidadId { get; set; }
    }
}