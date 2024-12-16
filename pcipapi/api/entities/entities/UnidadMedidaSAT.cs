using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("unidadesMedidaSAT")]
    public class UnidadMedidaSAT
    {
        public UnidadMedidaSAT()
        {
            this.unidadId = -1;
            this.unidadCodigo = string.Empty;
            this.unidadNombre = string.Empty;
            this.unidadActivo = false;
            this.objetoId = 15;
        }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }

        [Display(Name = "Activo")]
        [Column("unidadActivo")]
        public bool unidadActivo { get; set; }

        [StringLength(5)]
        [Display(Name = "Código")]
        [Column("unidadCodigo")]
        public string unidadCodigo { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("unidadId")]
        public int unidadId { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre")]
        [Column("unidadNombre")]
        public string unidadNombre { get; set; }
    }
}