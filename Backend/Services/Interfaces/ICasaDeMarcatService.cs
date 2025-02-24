using Backend.DTOs;
using Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface ICasaDeMarcatService
    {
        Task<IEnumerable<CasaDeMarcat>> GetAllCaseDeMarcatAsync();
        Task<CasaDeMarcat?> GetCasaDeMarcatByIdAsync(int id);
        Task<bool> AddCasaDeMarcatAsync(CasaDeMarcatDTO casaDto);
        Task<bool> UpdateCasaDeMarcatAsync(int id, CasaDeMarcatDTO casaDto);
        Task<bool> DeleteCasaDeMarcatAsync(int id);
    }
}
