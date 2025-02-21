using Backend.Data;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class CasaDeMarcatRepository(ApplicationDbContext context) : ICasaDeMarcatRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<CasaDeMarcat?> GetCasaDeMarcatByEmail(string email)
        {
            return await _context.CaseDeMarcat.FirstOrDefaultAsync(c => c.Email == email);
        }
        public async Task<CasaDeMarcat?> GetCasaDeMarcatByID(int id)
        {
            return await _context.CaseDeMarcat.FindAsync(id);
        }

        public async Task AddCasaDeMarcat(CasaDeMarcat casa)
        {
            await _context.CaseDeMarcat.AddAsync(casa);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCasaDeMarcat(CasaDeMarcat casa)
        {
            _context.CaseDeMarcat.Attach(casa);

            // 🔥 Nu modificăm `Email` sau `Password`
            _context.Entry(casa).Property(x => x.Email).IsModified = false;
            _context.Entry(casa).Property(x => x.Password).IsModified = false;

            await _context.SaveChangesAsync();
        }           

        public async Task DeleteCasaDeMarcat(CasaDeMarcat casa)
        {
            _context.CaseDeMarcat.Remove(casa);
            await _context.SaveChangesAsync();
        }



    }
}