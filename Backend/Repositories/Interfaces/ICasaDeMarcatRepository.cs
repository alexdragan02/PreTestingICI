using Backend.Models;
using Backend.Models.XML;

namespace Backend.Repositories.Interfaces
{
    public interface ICasaDeMarcatRepository
    {
        Task<CasaDeMarcat> GetCasaDeMarcatyIdAsync(int id);
        Task<List<CasaDeMarcat>> GetAllCaseDeMarcat();
        Task AddCasaDeMarcatAsync(CasaDeMarcat casa);
        Task UpdateCasaDeMarcatAsync(CasaDeMarcat casa);
        Task DeleteCasaDeMarcatASync(CasaDeMarcat casa);
         void MarkAsModified(CasaDeMarcat casa);
        Task AddMesajAsync(int casaId, Msj mesja);
    }
}