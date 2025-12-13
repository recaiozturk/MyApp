namespace MyApp.API.Options
{
    /// <summary>
    /// SuperAdmin kullanıcı ayarları için Options Pattern sınıfı
    /// </summary>
    public class SuperAdminSettings
    {
        public const string SectionName = "SuperAdmin";

        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}


