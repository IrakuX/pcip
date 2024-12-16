using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("puestos")]
    public class Puesto
    {
        public Puesto()
        {
            this.puestoId = -1;
            this.puestoCodigo = string.Empty;
            this.puestoNombre = string.Empty;
            this.puestoEncargadoEquipo = false;
            this.puestoEncargadoAlmacen = false;
            this.puestoActivo = false;
            this.objetoId = 2;
        }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }

        [Display(Name = "Activo")]
        [Column("puestoActivo")]
        public bool puestoActivo { get; set; }

        [StringLength(3)]
        [Display(Name = "Código")]
        [Column("puestoCodigo")]
        public string puestoCodigo { get; set; }

        [Display(Name = "Encargado de almacén")]
        [Column("puestoEncargadoAlmacen")]
        public bool puestoEncargadoAlmacen { get; set; }

        [Display(Name = "Encargado de equipo")]
        [Column("puestoEncargadoEquipo")]
        public bool puestoEncargadoEquipo { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("puestoId")]
        public int puestoId { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre")]
        [Column("puestoNombre")]
        public string puestoNombre { get; set; }
    }
}