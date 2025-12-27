namespace MyApp.Core.Options
{
    /// <summary>
    /// JWT token ayarları için Options Pattern sınıfı
    /// </summary>
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";

        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpirationInMinutes { get; set; } = 60;
    }
}
