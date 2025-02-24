using Backend.DTOs;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/casademarcat")]
    [ApiController]
    public class CasaDeMarcatController : ControllerBase
    {
        private readonly ICasaDeMarcatService _casaService;

        public CasaDeMarcatController(ICasaDeMarcatService casaService)
        {
            _casaService = casaService;
        }

        /// 📌 Listează toate casele de marcat
        [HttpGet("list")]
        public async Task<IActionResult> GetCaseDeMarcat()
        {
            var caseDeMarcat = await _casaService.GetAllCaseDeMarcatAsync();
            return Ok(caseDeMarcat);
        }

        /// 📌 Obține o casă de marcat după ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCasaDeMarcatById(int id)
        {
            var casa = await _casaService.GetCasaDeMarcatByIdAsync(id);
            if (casa == null) return NotFound("Casa de marcat not found.");

            return Ok(casa);
        }

        /// 📌 Adaugă o casă de marcat FĂRĂ autentificare
        [HttpPost("add")]
        public async Task<IActionResult> AddCasaDeMarcat([FromBody] CasaDeMarcatDTO casaDto)
        {
            if (string.IsNullOrEmpty(casaDto.UserId))
                return BadRequest("User ID is required."); // ✅ Verifică dacă `userId` este transmis

            var success = await _casaService.AddCasaDeMarcatAsync(casaDto);
            return success ? Ok("Casa de marcat added.") : BadRequest("Error adding casa de marcat.");
        }

        /// 📌 Actualizează o casă de marcat existentă
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCasaDeMarcat(int id, [FromBody] CasaDeMarcatDTO casaDto)
        {
            var success = await _casaService.UpdateCasaDeMarcatAsync(id, casaDto);
            return success ? Ok("Casa de marcat updated.") : NotFound("Casa de marcat not found.");
        }

        /// 📌 Șterge o casă de marcat după ID
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCasaDeMarcat(int id)
        {
            var success = await _casaService.DeleteCasaDeMarcatAsync(id);
            return success ? Ok("Casa de marcat deleted.") : NotFound("Casa de marcat not found.");
        }
    }
}
