using Backend.Data;
using Backend.Models;
using Backend.Models.XML;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class CasaDeMarcatRepository(ApplicationDbContext context) : ICasaDeMarcatRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task AddCasaDeMarcatAsync(CasaDeMarcat casa)
        {
            await _context.CaseDeMarcat.AddAsync(casa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCasaDeMarcatASync(CasaDeMarcat casa)
        {
            _context.CaseDeMarcat.Remove(casa);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CasaDeMarcat>> GetAllCaseDeMarcat()
        {
            return await _context.CaseDeMarcat.ToListAsync();
        }



        public async Task<CasaDeMarcat> GetCasaDeMarcatyIdAsync(int id)
        {
            return await _context.CaseDeMarcat.FindAsync(id);

        }

        public async Task UpdateCasaDeMarcatAsync(CasaDeMarcat casa)
        {
            // _context.CaseDeMarcat.Update(casa);
            
            await _context.SaveChangesAsync();
        }

        public void MarkAsModified(CasaDeMarcat casa)
        {
            _context.Entry(casa).State = EntityState.Modified;
        }
        public async Task AddMesajAsync(int casaId, Msj mesaj)
        {
            var casa = await _context.CaseDeMarcat.FindAsync(casaId);
            if (casa != null)
            {
                mesaj.CasaDeMarcatId = casaId;
                _context.Mesaje.Add(mesaj);
                await _context.SaveChangesAsync();
            }
        }
    }
}