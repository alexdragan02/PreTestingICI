namespace Backend.DTOs
{
    public record CasaDeMarcatDTO(
        string UserId,
        string Name,
        string? NUI=null,
        int TipProfil = 0,
        bool? TipReset = false,
        DateTime? DateTime = null,
        int? NrMinuteReconectare = null,
        string? DestinatieAmef = null,
        string? URLAmef = null
    )
    {
        public DateTime? DateTimeUtc => DateTime?.ToUniversalTime(); // ✅ Convertim în UTC direct în DTO
    }
}
