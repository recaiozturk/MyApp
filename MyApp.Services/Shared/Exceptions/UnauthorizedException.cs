namespace MyApp.Services.Shared.Exceptions
{
    /// <summary>
    /// Yetkilendirme hatası durumunda fırlatılacak exception
    /// </summary>
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
