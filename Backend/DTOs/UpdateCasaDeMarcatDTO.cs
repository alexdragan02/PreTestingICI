namespace Backend.DTOs
{
    public record UpdateCasaDeMarcatDTO(
        string Email, // 🔹 Trebuie să știm ce utilizator modificăm
        string? NUI = null,
        int? ProfileType = null,
        string? ProfileReset = null,
        DateTime? Date = null,
        int? ReconnectMinutes = null,
        string? DestinationAMEF = null,
        string? UrlAMEF = null
    );
}
