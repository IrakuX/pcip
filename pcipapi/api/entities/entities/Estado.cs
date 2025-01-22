using entities.models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace entities.entities
{
    [Table("estados")]
    public class Estado
    {
        [Display(Name = "Activo")]
        [Column("estadoActivo")]
        [JsonConverter(typeof(IntToBoolConverter))]
        public bool estadoActivo { get; set; } = false;

        [Key]
        [Display(Name = "Id")]
        [Column("estadoId")]
        public int estadoId { get; set; } = -1;

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("estadoNombre")]
        public string estadoNombre { get; set; } = string.Empty;

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; } = 7;
    }
}