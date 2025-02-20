namespace Backend.Models
{
    public class Client
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public long NUI { get; set; } 
        public int ProfileType { get; set; } 
        public bool ProfileReset { get; set; } 
        public DateTime? Date { get; set; } 

        // Fields for ProfileType = 1
        public int? ReconnectMinutes { get; set; }
        public string? DestinationAMEF { get; set; }
        public string? UrlAMEF { get; set; }
    }
}
