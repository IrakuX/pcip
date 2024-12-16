using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("impuestos")]
    public class Impuesto
    {
        public Impuesto()
        {
            this.impuestoId = -1;
            this.impuestoGrupoId = -1;
            this.impuestoNombre = string.Empty;
            this.impuestoNombreCorto = string.Empty;
            this.impuestoDescripcion = string.Empty;
            this.impuestoCodigo = string.Empty;
            this.impuestoIdOpuesto = -1;
            this.impuestoActivo = false;
            this.objetoId = 13;
        }

        [Key]
        [Display(Name = "Id")]
        [Column("impuestoId")]
        public int impuestoId { get; set; }

        [ForeignKey("impuestoGrupoId")]
        [Display(Name = "Grupo")]
        [Column("impuestoGrupoId")]
        public int impuestoGrupoId { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre")]
        [Column("impuestoNombre")]
        public string impuestoNombre { get; set; }

        [StringLength(5)]
        [Display(Name = "Código")]
        [Column("impuestoNombreCorto")]
        public string impuestoNombreCorto { get; set; }

        [StringLength(500)]
        [Display(Name = "Descripción")]
        [Column("impuestoDescripcion")]
        public string impuestoDescripcion { get; set; }

        [StringLength(3)]
        [Display(Name = "Código")]
        [Column("impuestoCodigo")]
        public string impuestoCodigo { get; set; }

        [Display(Name = "Opuesto")]
        [Column("impuestoIdOpuesto")]
        public int impuestoIdOpuesto { get; set; }

        [Display(Name = "Activo")]
        [Column("impuestoActivo")]
        public bool impuestoActivo { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }
    }
}