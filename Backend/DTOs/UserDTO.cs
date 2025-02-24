namespace Backend.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<CasaDeMarcatDTO> CaseDeMarcat { get; set; }
    }
}