using Backend.Data;
using Backend.Models.XML;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    public class MsjRepository : IMsjRepository
    {
        private readonly ApplicationDbContext _context;

        public MsjRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Msj>> GetAllMesajeAsync()
        {
            return await _context.Mesaje.ToListAsync();
        }

        public async Task<Msj> GetMesajByIdAsync(string id)
        {
            return await _context.Mesaje.FindAsync(id);
        }

        public async Task AddMesajAsync(Msj mesaj)
        {
            await _context.Mesaje.AddAsync(mesaj);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
