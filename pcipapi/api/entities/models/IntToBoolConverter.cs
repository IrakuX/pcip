using System.Text.Json;
using System.Text.Json.Serialization;

namespace entities.models
{
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