using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.models.Usuario
{
    public class UsuarioLoginViewModel
    {
        public UsuarioLoginViewModel()
        {
            this.usuarioEmail = string.Empty;
            this.usuarioContrasena = string.Empty;
            this.usuarioRecuerdame = false;
        }

        [Display(Name = "Contraseña")]
        [Column("usuarioContrasena")]
        public string usuarioContrasena { get; set; }

        [Display(Name = "Email")]
        [Column("usuarioEmail")]
        public string usuarioEmail { get; set; }

        [Display(Name = "Recuerdame")]
        [Column("usuarioRecuerdame")]
        public bool usuarioRecuerdame { get; set; }
    }
}