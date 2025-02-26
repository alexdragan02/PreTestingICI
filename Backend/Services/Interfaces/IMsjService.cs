using Backend.Models.XML;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface IMsjService
    {
        Task<IEnumerable<Msj>> GetAllMesajeAsync();
        Task<Msj> GetMesajByIdAsync(string id);
        Task<IEnumerable<Msj>> GetMesajeByCasaDeMarcatIdAsync(int casaDeMarcatId);
        Task GenerateAndSaveRandomMesaje(int casaDeMarcatId, int count);
    }
}
