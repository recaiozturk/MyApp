namespace MyApp.Services.Shared.Exceptions
{
    /// <summary>
    /// Geçersiz istek durumunda fırlatılacak exception
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }
}
