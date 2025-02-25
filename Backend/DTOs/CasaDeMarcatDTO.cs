namespace Backend.DTOs
{
    public record CasaDeMarcatDTO(
        string UserId,
        string Name,
        int TipProfil ,
        bool TipReset , 
        string? NUI = null,
        DateTime? DateTime = null,
        int? NrMinuteReconectare = null,
        string? DestinatieAmef = null,
        string? URLAmef = null
    )
    {
        public DateTime? DateTimeUtc => DateTime?.ToUniversalTime();
    }
}