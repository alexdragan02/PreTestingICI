using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.XML;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MsjController : ControllerBase
    {
        private readonly IMsjService _msjService;

        public MsjController(IMsjService msjService)
        {
            _msjService = msjService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Msj>>> GetAllMesaje()
        {
            var mesaje = await _msjService.GetAllMesajeAsync();
            if (mesaje == null)
            {
                return NotFound("Nu s-au găsit mesaje.");
            }
            return Ok(mesaje);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Msj>> GetMesajById(string id)
        {
            var mesaj = await _msjService.GetMesajByIdAsync(id);
            if (mesaj == null)
            {
                return NotFound($"Mesajul cu ID-ul {id} nu a fost găsit.");
            }
            return Ok(mesaj);
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateMesaje([FromQuery] int count = 5)
        {
            if (count <= 0)
            {
                return BadRequest("Numărul de mesaje trebuie să fie mai mare decât 0.");
            }

            await _msjService.GenerateAndSaveRandomMesaje(count);
            return Ok($"S-au generat {count} mesaje random.");
        }
    }
}
