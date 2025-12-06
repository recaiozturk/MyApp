namespace MyApp.API.Options
{
    /// <summary>
    /// Veritabanı bağlantı ayarları için Options Pattern sınıfı
    /// </summary>
    public class DatabaseSettings
    {
        public const string SectionName = "ConnectionStrings";

        public string DefaultConnection { get; set; } = string.Empty;
    }
}

