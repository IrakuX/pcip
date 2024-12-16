using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace entities.entities
{
    [Table("unidadesMedida")]
    public class UnidadMedida
    {
        public UnidadMedida()
        {
            this.unidadMedidaId = -1;
            this.unidadMedidaCodigo = string.Empty;
            this.unidadMedidaNombre = string.Empty;
            this.unidadMedidaFactor = 0M;
            this.unidadMedidaPeso = 0M;
            this.unidadMedidaVolumen = 0M;
            this.unidadMedidaActivo = false;
            this.objetoId = 14;
        }

        [Key]
        [Display(Name = "Id")]
        [Column("unidadMedidaId")]
        public int unidadMedidaId { get; set; }

        [StringLength(5)]
        [Display(Name = "Código")]
        [Column("unidadMedidaCodigo")]
        public string unidadMedidaCodigo { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("unidadMedidaNombre")]
        public string unidadMedidaNombre { get; set; }

        [Display(Name = "Factor")]
        [Column("unidadMedidaFactor")]
        public decimal unidadMedidaFactor { get; set; }

        [Display(Name = "Peso")]
        [Column("unidadMedidaPeso")]
        public decimal unidadMedidaPeso { get; set; }

        [Display(Name = "Volumen")]
        [Column("unidadMedidaVolumen")]
        public decimal unidadMedidaVolumen { get; set; }

        [Display(Name = "Activo")]
        [Column("unidadMedidaActivo")]
        public bool unidadMedidaActivo { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }
    }
}