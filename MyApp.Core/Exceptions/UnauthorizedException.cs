namespace MyApp.Core.Exceptions
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


