using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.XML;

namespace Backend.Repositories.Interfaces
{
    public interface IMsjRepository
    {
          Task<IEnumerable<Msj>> GetAllMesajeAsync();
        Task<Msj> GetMesajByIdAsync(string id);
        Task AddMesajAsync(Msj mesaj);
        
        Task SaveChangesAsync();
    }
}