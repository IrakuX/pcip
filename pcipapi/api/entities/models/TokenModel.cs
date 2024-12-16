using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace entities.models
{
    public class TokenModel
    {
        [Column("token")]
        [Display(Name = "Token")]
        [JsonPropertyName("token")]
        public string token { get; set; } = string.Empty;

        [Column("refreshToken")]
        [Display(Name = "Refresh token")]
        [JsonPropertyName("refreshToken")]
        public Guid refreshToken { get; set; } = Guid.Empty;
    }
}