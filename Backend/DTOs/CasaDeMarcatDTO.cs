namespace Backend.DTOs
{
    public record CasaDeMarcatDTO(
        string? NUI,
        string Email,
        string Password,
        int? ProfileType,
        string? ProfileReset,
        DateTime? Date,
        int? ReconnectMinutes,
        string? DestinationAMEF,
        string? UrlAMEF
    );
}
