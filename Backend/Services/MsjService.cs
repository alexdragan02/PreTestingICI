using Backend.Models.XML;
using Backend.Models.XML.Bon;
using Backend.Models.XML.Me;
using Backend.Models.XML.rB;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Bogus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class MsjService : IMsjService
    {
        private readonly IMsjRepository _msjRepository;

        public MsjService(IMsjRepository msjRepository)
        {
            _msjRepository = msjRepository;
        }

        public async Task<IEnumerable<Msj>> GetAllMesajeAsync()
        {
            return await _msjRepository.GetAllMesajeAsync();
        }

        public async Task<Msj> GetMesajByIdAsync(string id)
        {
            return await _msjRepository.GetMesajByIdAsync(id);
        }

        public async Task GenerateAndSaveRandomMesaje(int count)
        {
            var faker = new Faker("ro");
            var mesaje = new List<Msj>();

            for (int i = 0; i < count; i++)
            {
                var mesaj = new Msj
                {
                    IdM = faker.Random.AlphaNumeric(25),
                    Bon = new Bon
                    {
                        IdB = faker.Random.AlphaNumeric(25),
                        TotB = faker.Finance.Amount(10, 500), 
                        TotTva = faker.Finance.Amount(1, 50),
                        Cote = new List<Cote>
                        {
                            new Cote { Cota = 19, Tva = faker.Finance.Amount(1, 20) },
                            new Cote { Cota = 5, Tva = faker.Finance.Amount(1, 10) }
                        }
                    },
                    RB = new RB
                    {
                        IdR = faker.Random.AlphaNumeric(25),
                        NrAv = faker.Random.Int(1, 5),
                        NrB = 1,
                        TotB = (float)faker.Finance.Amount(10, 500),
                        TotTva = (float)faker.Finance.Amount(1, 50),
                        MonRef = "RON",
                        Pl = new PL
                        {
                            TipP = faker.Random.Int(1, 3),
                            ValPl = (float)faker.Finance.Amount(10, 500),
                            MonPl = "RON"
                        }
                    }
                };

                mesaje.Add(mesaj);
            }

            foreach (var mesaj in mesaje)
            {
                await _msjRepository.AddMesajAsync(mesaj);
            }

            await _msjRepository.SaveChangesAsync();
        }
    }
}
