using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace entities.entities
{
    [Table("empleados")]
    public class Empleado
    {
        public Empleado()
        {
            this.empleadoId = -1;
            this.empleadoNombre = string.Empty;
            this.empleadoApellidos = string.Empty;
            this.empleadoFechaNacimiento = null;
            this.empleadoGenero = 0;
            this.empleadoFechaIngreso = null;
            this.empleadoEmail = string.Empty;
            this.empleadoTelefono = string.Empty;
            this.empleadoMovil = string.Empty;
            this.puestoId = null;
            this.empleadoActivo = false;
            this.objetoId = 6;
        }

        [Display(Name = "Activo")]
        [Column("empleadoActivo")]
        public bool empleadoActivo { get; set; }

        [StringLength(300)]
        [Display(Name = "Apellidos")]
        [Column("empleadoApellidos")]
        public string empleadoApellidos { get; set; }

        [StringLength(150)]
        [Display(Name = "Email")]
        [Column("empleadoEmail")]
        public string empleadoEmail { get; set; }

        [Display(Name = "Fecha de ingreso")]
        [Column("empleadoFechaIngreso")]
        public DateTime? empleadoFechaIngreso { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Column("empleadoFechaNacimiento")]
        public DateTime? empleadoFechaNacimiento { get; set; }

        [Display(Name = "Genero")]
        [Column("empleadoGenero")]
        public int empleadoGenero { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("empleadoId")]
        public int empleadoId { get; set; }

        [StringLength(15)]
        [Display(Name = "Movil")]
        [Column("empleadoMovil")]
        public string empleadoMovil { get; set; }

        [StringLength(150)]
        [Display(Name = "Nombre")]
        [Column("empleadoNombre")]
        public string empleadoNombre { get; set; }

        [StringLength(15)]
        [Display(Name = "Telefono")]
        [Column("empleadoTelefono")]
        public string empleadoTelefono { get; set; }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }

        [ForeignKey("puestoId")]
        [Display(Name = "Puesto")]
        [Column("puestoId")]
        public int? puestoId { get; set; }
    }
}