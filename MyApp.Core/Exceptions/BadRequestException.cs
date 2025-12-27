namespace MyApp.Core.Exceptions
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



