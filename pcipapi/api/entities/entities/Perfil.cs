using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace entities.entities
{
    [Table("perfiles")]
    public class Perfil
    {
        public Perfil()
        {
            this.perfilId = -1;
            this.perfilNombre = string.Empty;
            this.perfilActivo = false;
            this.objetoId = 1;
        }

        [ForeignKey("objetoId")]
        [Display(Name = "Objeto")]
        [Column("objetoId")]
        public int objetoId { get; set; }

        [Display(Name = "Activo")]
        [Column("perfilActivo")]
        [JsonConverter(typeof(IntToBoolConverter))]
        public bool perfilActivo { get; set; }

        [Key]
        [Display(Name = "Id")]
        [Column("perfilId")]
        public int perfilId { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre")]
        [Column("perfilNombre")]
        public string perfilNombre { get; set; }
    }

    public class IntToBoolConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetInt32() != 0; // Convierte 1 a true y 0 a false
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value ? 1 : 0); // Convierte true a 1 y false a 0
        }
    }
}