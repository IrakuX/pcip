using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    public class Almacen
    {
        public Almacen()
        {
            this.almacenId = -1;
            this.almacenPadreId = null;
            this.almacenNombre = string.Empty;
            this.almacenDescripcion = string.Empty;
            this.almacenSecuenciaOrden = 0;
            this.empleadoId = null;
            this.almacenParaActivos = false;
            this.almacenActivo = false;
            this.objetoId = 16;
        }

        [Display(Name = "Activo")]
        [Column("almacenActivo")]
        public bool almacenActivo { get; set; }

        [StringLength(1000)]
        [Display(Name = "Descripción")]
        [Column("almacenDescripcion")]
        public string almacenDescripcion { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("almacenId")]
        public int almacenId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("almacenNombre")]
        public string almacenNombre { get; set; }

        [Display(Name = "Padre id")]
        [Column("almacenPadreId")]
        public int? almacenPadreId { get; set; }

        [Display(Name = "Para activos")]
        [Column("almacenParaActivos")]
        public bool almacenParaActivos { get; set; }

        [Display(Name = "Secuencia orden")]
        [Column("almacenSecuenciaOrden")]
        public int almacenSecuenciaOrden { get; set; }

        [ForeignKey("empleadoId")]
        [Display(Name = "Empleado")]
        [Column("empleadoId")]
        public int? empleadoId { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }
    }
}