using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/casademarcat")]
    [ApiController]
    public class CasaDeMarcatController : ControllerBase
    {
        private readonly CasaDeMarcatService _service;

        public CasaDeMarcatController(CasaDeMarcatService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var casa = await _service.LoginAsync(request.Email, request.Password);
            if (casa == null) return Unauthorized("Email sau parolă incorectă.");

            return Ok(new 
            { 
                Message = "Login reușit",
                Casa = casa // ✅ Returnăm toate datele pentru frontend
            });
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCasaDeMarcatDTO request)
        {
            await _service.AddCasaDeMarcatAsync(request);
            return Ok(new { Message = "Cont creat cu succes." });
        }


        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCasaDeMarcatDTO request)
        {
            try
            {
                await _service.UpdateCasaDeMarcatAsync(request);
                return Ok(new { Message = "Atribute actualizate cu succes." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Casa de marcat nu a fost găsită.");
            }
        }


        // ✅ Ștergere casa de marcat
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteCasaDeMarcatAsync(id);
                return Ok(new { Message = "Casa de marcat ștearsă." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Casa de marcat nu a fost găsită.");
            }
        }
    }
}
