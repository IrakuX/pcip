namespace entities.models
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public TimeSpan TokenLifetime { get; set; } = TimeSpan.Zero;
    }
}