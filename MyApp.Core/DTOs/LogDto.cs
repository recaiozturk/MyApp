namespace MyApp.Core.DTOs
{
    public class LogDto
    {
        public long Id { get; set; }
        public string Application { get; set; } = string.Empty;
        public DateTime Logged { get; set; }
        public string Level { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? Logger { get; set; }
        public string? Callsite { get; set; }
        public string? Exception { get; set; }
        public string? Properties { get; set; }
    }
}


