using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.XML;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using System.IO;

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

        /// <summary>
        /// Obține toate mesajele XML.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Msj>>> GetAllMesaje()
        {
            var mesaje = await _msjService.GetAllMesajeAsync();
            if (mesaje == null || !mesaje.Any())
            {
                return NotFound("Nu s-au găsit mesaje.");
            }
            return Ok(mesaje);
        }

        /// <summary>
        /// Obține mesajele XML sub formă de fișier XML.
        /// </summary>
        [HttpGet("xml")]
        public async Task<IActionResult> GetAllMesajeXml()
        {
            var mesaje = await _msjService.GetAllMesajeAsync();
            if (mesaje == null || !mesaje.Any())
            {
                return NotFound("Nu s-au găsit mesaje.");
            }

            try
            {
                var xmlSerializer = new XmlSerializer(typeof(List<Msj>), new XmlRootAttribute("MsjCollection"));
                using (var stringWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(stringWriter, mesaje);
                    return Content(stringWriter.ToString(), "application/xml");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Eroare la serializarea XML: {ex.Message}");
            }
        }

        /// <summary>
        /// Obține un mesaj după ID.
        /// </summary>
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Msj>> GetMesajById(string id)
        {
            var mesaj = await _msjService.GetMesajByIdAsync(id);
            if (mesaj == null)
            {
                return NotFound($"Mesajul cu ID-ul {id} nu a fost găsit.");
            }
            return Ok(mesaj);
        }

        /// <summary>
        /// Obține toate mesajele asociate unei case de marcat.
        /// </summary>
        [HttpGet("by-casa")]
        public async Task<ActionResult<IEnumerable<Msj>>> GetMesajeByCasa([FromQuery] int casaDeMarcatId)
        {
            var mesaje = await _msjService.GetMesajeByCasaDeMarcatIdAsync(casaDeMarcatId);
            if (mesaje == null || !mesaje.Any())
            {
                return NotFound($"Nu s-au găsit mesaje pentru casa de marcat cu ID-ul {casaDeMarcatId}.");
            }
            return Ok(mesaje);
        }

        /// <summary>
        /// Generează mesaje pentru o casă de marcat.
        /// </summary>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateMesaje([FromQuery] int casaDeMarcatId, [FromQuery] int count)
        {
            if (count <= 0)
            {
                return BadRequest("Numărul de mesaje trebuie să fie mai mare decât 0.");
            }

            await _msjService.GenerateAndSaveRandomMesaje(casaDeMarcatId, count);
            return Ok($"S-au generat {count} mesaje pentru casa de marcat cu ID-ul {casaDeMarcatId}.");
        }
    }
}
