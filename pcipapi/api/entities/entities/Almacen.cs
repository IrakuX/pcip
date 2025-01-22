using entities.models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonConverter(typeof(IntToBoolConverter))]
        public bool almacenActivo { get; set; } = false;

        [StringLength(1000)]
        [Display(Name = "Descripción")]
        [Column("almacenDescripcion")]
        public string almacenDescripcion { get; set; } = string.Empty;

        [Key]
        [Display(Name = "Id")]
        [Column("almacenId")]
        public int almacenId { get; set; } = -1;

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("almacenNombre")]
        public string almacenNombre { get; set; } = string.Empty;

        [Display(Name = "Padre id")]
        [Column("almacenPadreId")]
        public int? almacenPadreId { get; set; } = null;

        [Display(Name = "Para activos")]
        [Column("almacenParaActivos")]
        [JsonConverter(typeof(IntToBoolConverter))]
        public bool almacenParaActivos { get; set; } = false;

        [Display(Name = "Secuencia orden")]
        [Column("almacenSecuenciaOrden")]
        public int almacenSecuenciaOrden { get; set; } = 0;

        [ForeignKey("empleadoId")]
        [Display(Name = "Empleado")]
        [Column("empleadoId")]
        public int? empleadoId { get; set; } = null;

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; } = 16;
    }
}