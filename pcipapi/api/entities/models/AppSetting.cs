namespace entities.models
{
    public class AppSetting
    {
        public string audience { get; set; } = string.Empty;
        public string directorioFisico { get; set; } = string.Empty;
        public string directorioVirtual { get; set; } = string.Empty;
        public string googleMap { get; set; } = string.Empty;
        public string issuer { get; set; } = string.Empty;
        public string key { get; set; } = string.Empty;
        public string sendgridKey { get; set; } = string.Empty;
        public string sistemaURL { get; set; } = string.Empty;
        public string reporteURL { get; set; } = string.Empty;
        public System.TimeSpan tokenLifetime { get; set; } = System.TimeSpan.Zero;
    }
}