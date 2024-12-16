using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.entities
{
    [Table("usuarioToken")]
    public class UsuarioToken
    {
        [Key]
        [Display(Name = "ID del Usuario")]
        [Column("usuarioId")]
        public int usuarioId { get; set; }

        [Key]
        [Display(Name = "JWT ID del Usuario")]
        [Column("usuarioJwtId")]
        public Guid usuarioJwtId { get; set; }

        [Display(Name = "Token del Usuario")]
        [Column("usuarioToken")]
        public Guid usuarioToken { get; set; }

        [Column("usuarioTokenUsado")]
        [Display(Name = "Usado")]
        public bool usuarioTokenUsado { get; set; }

        [Display(Name = "Fecha de Creación del Token")]
        [Column("usuarioTokenFechaCreacion")]
        public DateTime usuarioTokenFechaCreacion { get; set; } = DateTime.Now;

        [Display(Name = "Fecha de Expiración del Token")]
        [Column("usuarioTokenFechaExpiracion")]
        public DateTime usuarioTokenFechaExpiracion { get; set; } = DateTime.Now;
    }
}