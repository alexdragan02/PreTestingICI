using Backend.DTOs;
using Backend.Models;
using Backend.Models.XML;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Backend.Models.XML;

namespace Backend.Services
{
    public class CasaDeMarcatService : ICasaDeMarcatService
    {
        private readonly ICasaDeMarcatRepository _casaRepository;

        public CasaDeMarcatService(ICasaDeMarcatRepository casaRepository)
        {
            _casaRepository = casaRepository;
        }

        public async Task<IEnumerable<CasaDeMarcat>> GetAllCaseDeMarcatAsync()
        {
            return await _casaRepository.GetAllCaseDeMarcat();
        }

        public async Task<CasaDeMarcat?> GetCasaDeMarcatByIdAsync(int id)
        {
            return await _casaRepository.GetCasaDeMarcatyIdAsync(id);
        }

       public async Task<bool> AddCasaDeMarcatAsync(CasaDeMarcatDTO casaDto)
        {
            var casa = new CasaDeMarcat
            {
                Name = casaDto.Name,
                UserId = casaDto.UserId,
                TipProfil = 0,
                TipReset = false,
                MesajXML = casaDto.MesajXML ?? new List<Msj>()
            };

            await _casaRepository.AddCasaDeMarcatAsync(casa);
            return true;
        }




       public async Task<bool> UpdateCasaDeMarcatAsync(int id, CasaDeMarcatDTO casaDto)
        {
            var casa = await _casaRepository.GetCasaDeMarcatyIdAsync(id);
            if (casa == null)
            {
                return false;
            }

            // Actualizăm toate proprietățile
            casa.Name = casaDto.Name;
            casa.NUI = casaDto.NUI;
            casa.TipProfil = casaDto.TipProfil;
            casa.TipReset = casaDto.TipReset;
            casa.DateTime = casaDto.DateTime;
            casa.NrMinuteReconectare = casaDto.NrMinuteReconectare;
            casa.DestinatieAmef = casaDto.DestinatieAmef;
            casa.URLAmef = casaDto.URLAmef;
            casa.MesajXML = casaDto.MesajXML ?? new List<Models.XML.Msj>();
            // Marchează explicit proprietățile ca modificate
            // _casaRepository.MarkAsModified(casa);

            await _casaRepository.UpdateCasaDeMarcatAsync(casa);
            return true;
        }
        public async Task<bool> AddMesajToCasaDeMarcatAsync(int casaId, Msj mesaj)
        {
            await _casaRepository.AddMesajAsync(casaId, mesaj);
            return true;
        }

        public async Task<bool> DeleteCasaDeMarcatAsync(int id)
        {
            var casa = await _casaRepository.GetCasaDeMarcatyIdAsync(id);
            if (casa == null)
            {
                return false;
            }

            await _casaRepository.DeleteCasaDeMarcatASync(casa);
            return true;
        }
    }
}
