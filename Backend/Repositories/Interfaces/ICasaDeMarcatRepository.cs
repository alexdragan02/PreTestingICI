using Backend.Models;

namespace Backend.Repositories.Interfaces{
public interface ICasaDeMarcatRepository{
    Task<CasaDeMarcat?> GetCasaDeMarcatByEmail(string email);
    Task<CasaDeMarcat?> GetCasaDeMarcatByID(int id);
    Task AddCasaDeMarcat(CasaDeMarcat casaDeMarcat);
    Task UpdateCasaDeMarcat(CasaDeMarcat casaDeMarcat);
    Task DeleteCasaDeMarcat(CasaDeMarcat casaDeMarcat);
}
}
