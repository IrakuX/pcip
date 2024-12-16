using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace entities.models.Usuario
{
    public class UsuarioSesionViewModel : entities.Usuario
    {
        [Column("empleadoNombreApellidos")]
        [Display(Name = "Empleaddo nombre apellidos")]
        [JsonPropertyName("empleadoNombreApellidos")]
        public string empleadoNombreApellidos { get; set; } = string.Empty;

        [Column("perfilNombre")]
        [Display(Name = "Perfil nombre")]
        [JsonPropertyName("perfilNombre")]
        public string perfilNombre { get; set; } = string.Empty;

        [Column("usuarioNombreApellidos")]
        [Display(Name = "Usuario nombre apellidos")]
        [JsonPropertyName("usuarioNombreApellidos")]
        public string usuarioNombreApellidos
        {
            get
            {
                return string.Format("{0} {1}", this.usuarioNombre, this.usuarioApellidos);
            }
        }
    }
}