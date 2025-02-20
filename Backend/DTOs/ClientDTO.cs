namespace Backend.DTOs
{
    public class ClientDto
    {
        public Guid Id { get; set; } 
        public long NUI { get; set; } 
        public int ProfileType { get; set; }
        public bool ProfileReset { get; set; }
        public DateTime? Date { get; set; }
        public int? ReconnectMinutes { get; set; }
        public string? DestinationAMEF { get; set; }
        public string? UrlAMEF { get; set; }

        // ✅ Add a parameterless constructor for deserialization
        public ClientDto() { }

        public ClientDto(Backend.Models.Client client)
        {
            Id = client.Id;
            NUI = client.NUI;
            ProfileType = client.ProfileType;
            ProfileReset = client.ProfileReset;
            Date = client.Date;
            ReconnectMinutes = client.ReconnectMinutes;
            DestinationAMEF = client.DestinationAMEF;
            UrlAMEF = client.UrlAMEF;
        }
    }
}
