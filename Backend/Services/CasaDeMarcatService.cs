using Backend.DTOs;
using Backend.Models;
using Backend.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class CasaDeMarcatService(ICasaDeMarcatRepository casaDeMarcatRepository)
    {
        private readonly ICasaDeMarcatRepository _casaDeMarcatRepository = casaDeMarcatRepository;

        public async Task<CasaDeMarcatDTO?> LoginAsync(string email, string password)
        {
            var casa = await _casaDeMarcatRepository.GetCasaDeMarcatByEmail(email);
            if (casa == null || casa.Password != password)
                return null; // Email sau parolă incorectă

            // ✅ Convertim modelul în DTO și returnăm toate datele
            return new CasaDeMarcatDTO(
                casa.NUI,
                casa.Email,
                casa.Password,
                casa.ProfileType,
                casa.ProfileReset,
                casa.Date,
                casa.ReconnectMinutes,
                casa.DestinationAMEF,
                casa.UrlAMEF
            );
        }
        public async Task AddCasaDeMarcatAsync(CreateCasaDeMarcatDTO dto)
        {
            var casa = new CasaDeMarcat(dto.Email, dto.Password);
            await _casaDeMarcatRepository.AddCasaDeMarcat(casa);
        }


        public async Task UpdateCasaDeMarcatAsync(UpdateCasaDeMarcatDTO dto)
        {
            var casa = await _casaDeMarcatRepository.GetCasaDeMarcatByEmail(dto.Email);

            if (casa == null)
                throw new KeyNotFoundException("Casa de marcat nu a fost găsită.");

            // ✅ Modificăm doar atributele primite în request
            if (dto.NUI != null) casa.NUI = dto.NUI;
            if (dto.ProfileType.HasValue) casa.ProfileType = dto.ProfileType;
            if (dto.ProfileReset != null) casa.ProfileReset = dto.ProfileReset;
            if (dto.Date.HasValue) casa.Date = dto.Date;
            if (dto.ReconnectMinutes.HasValue) casa.ReconnectMinutes = dto.ReconnectMinutes;
            if (dto.DestinationAMEF != null) casa.DestinationAMEF = dto.DestinationAMEF;
            if (dto.UrlAMEF != null) casa.UrlAMEF = dto.UrlAMEF;

            await _casaDeMarcatRepository.UpdateCasaDeMarcat(casa);
        }




        // ✅ Ștergere casa de marcat
        public async Task DeleteCasaDeMarcatAsync(int id)
        {
            var casa = await _casaDeMarcatRepository.GetCasaDeMarcatByID(id);
            if (casa == null) throw new KeyNotFoundException("Casa de marcat nu există.");

            await _casaDeMarcatRepository.DeleteCasaDeMarcat(casa);
        }
    }
}
