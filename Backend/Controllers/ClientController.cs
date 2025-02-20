using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetClientById(Guid id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
                return NotFound("Client not found.");

            return Ok(client);
        }

        [HttpGet("nui/{nui}")]
        public async Task<ActionResult<ClientDto>> GetClientByNUI(long nui)
        {
            var client = await _clientService.GetClientByNUIAsync(nui);
            if (client == null)
                return NotFound("Client not found.");

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult> AddClient([FromBody] ClientDto newClient)
        {
            await _clientService.AddClientAsync(newClient);
            return CreatedAtAction(nameof(GetClientByNUI), new { nui = newClient.NUI }, newClient);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient(Guid id, [FromBody] ClientDto updatedClient)
        {
            await _clientService.UpdateClientAsync(id, updatedClient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(Guid id)
        {
            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }
    }
}
