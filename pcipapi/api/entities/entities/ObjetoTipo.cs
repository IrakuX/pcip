using entities.models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace entities.entities
{
    [Table("objetoTipos")]
    public class ObjetoTipo
    {
        [Display(Name = "Activo")]
        [Column("objetoTipoActivo")]
        [JsonConverter(typeof(IntToBoolConverter))]
        public bool objetoTipoActivo { get; set; } = false;

        [Key]
        [Required]
        [Display(Name = "Id")]
        [Column("objetoTipoId")]
        public int objetoTipoId { get; set; } = -1;

        [StringLength(50)]
        [Display(Name = "Nombre")]
        [Column("objetoTipoNombre")]
        public string objetoTipoNombre { get; set; } = string.Empty;
    }
}