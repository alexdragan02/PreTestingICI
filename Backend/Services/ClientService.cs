using Backend.DTOs;
using Backend.Models;
using Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllClientsAsync();
            return clients.Select(client => new ClientDto(client)).ToList();
        }

        public async Task<ClientDto?> GetClientByIdAsync(Guid id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            return client == null ? null : new ClientDto(client);
        }

        public async Task<ClientDto?> GetClientByNUIAsync(long nui)
        {
            var client = await _clientRepository.GetClientByNUIAsync(nui);
            return client == null ? null : new ClientDto(client);
        }

        public async Task AddClientAsync(ClientDto clientDTO)
        {
            var client = new Client
            {
                NUI = clientDTO.NUI,
                ProfileType = clientDTO.ProfileType,
                ProfileReset = clientDTO.ProfileReset,
                Date = clientDTO.Date,
                ReconnectMinutes = clientDTO.ReconnectMinutes,
                DestinationAMEF = clientDTO.DestinationAMEF,
                UrlAMEF = clientDTO.UrlAMEF
            };
            await _clientRepository.AddClientAsync(client);
        }

        public async Task UpdateClientAsync(Guid id, ClientDto clientDTO)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            if (client == null)
            {
                throw new Exception("Client not found");
            }
            client.NUI = clientDTO.NUI;
            client.ProfileType = clientDTO.ProfileType;
            client.ProfileReset = clientDTO.ProfileReset;
            client.Date = clientDTO.Date;
            client.ReconnectMinutes = clientDTO.ReconnectMinutes;
            client.DestinationAMEF = clientDTO.DestinationAMEF;
            client.UrlAMEF = clientDTO.UrlAMEF;
            await _clientRepository.UpdateClientAsync(client);
        }

        public async Task DeleteClientAsync(Guid id)
        {
            await _clientRepository.DeleteClientAsync(id);
        }
    }
}
