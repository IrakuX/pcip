using System.Text.Json.Serialization;

namespace entities.models
{
    public class ResponseData<T>
    {
        [JsonPropertyNameAttribute("data")]
        public T? data { get; set; }

        [JsonPropertyNameAttribute("total")]
        public int total { get; set; }
    }
}