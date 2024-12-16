using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace entities.entities
{
    [Table("usuarios")]
    public class Usuario
    {
        public Usuario()
        {
            this.usuarioId = -1;
            this.usuarioNombre = string.Empty;
            this.usuarioApellidos = string.Empty;
            this.usuarioEmail = string.Empty;
            this.usuarioContrasena = null;
            this.usuarioContrasenaRecuperacion = string.Empty;
            this.usuarioToken = string.Empty;
            this.usuarioActivo = false;
            this.perfilId = null;
            this.empleadoId = null;
            this.objetoId = 3;
        }

        [ForeignKey("empleadoId")]
        [Display(Name = "Empleado")]
        [Column("empleadoId")]
        public int? empleadoId { get; set; }

        [ForeignKey("objetoId")]
        [Column("objetoId")]
        [Display(Name = "Objeto")]
        public int objetoId { get; set; }

        [ForeignKey("perfilId")]
        [Display(Name = "Perfil")]
        [Column("perfilId")]
        public int? perfilId { get; set; }

        [Display(Name = "Activo")]
        [Column("usuarioActivo")]
        public bool usuarioActivo { get; set; }

        [StringLength(300)]
        [Display(Name = "Apellidos del Usuario")]
        [Column("usuarioApellidos")]
        public string usuarioApellidos { get; set; }

        [Display(Name = "Contraseña del Usuario")]
        [Column("usuarioContrasena")]
        public byte[]? usuarioContrasena { get; set; }

        [StringLength(15)]
        [Display(Name = "Contraseña de Recuperación")]
        [Column("usuarioContrasenaRecuperacion")]
        public string usuarioContrasenaRecuperacion { get; set; }

        [StringLength(150)]
        [Display(Name = "Email del Usuario")]
        [Column("usuarioEmail")]
        public string usuarioEmail { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("usuarioId")]
        public int usuarioId { get; set; }

        [StringLength(200)]
        [Display(Name = "Nombre del Usuario")]
        [Column("usuarioNombre")]
        public string usuarioNombre { get; set; }

        [StringLength(50)]
        [Display(Name = "Token del Usuario")]
        [Column("usuarioToken")]
        public string usuarioToken { get; set; }
    }
}