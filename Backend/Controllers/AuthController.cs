using Backend.Models;
using Backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginModel model)
{
    var user = await _userManager.FindByEmailAsync(model.Email);
    if (user == null)
        return Unauthorized("Invalid email or password");

    var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
    if (!result.Succeeded)
        return Unauthorized("Invalid email or password");

    // Creează DTO pentru utilizator fără ciclu infinit
   var userDto = new UserDTO
    {
        Id = user.Id,
        Email = user.Email,
        CaseDeMarcat = await _context.CaseDeMarcat
            .Where(c => c.UserId == user.Id)
            .Select(c => new CasaDeMarcatDTO(
                user.Id,  // ✅ UserId este necesar
                c.Name,
                c.NUI,
                c.TipProfil,
                c.TipReset,
                c.DateTime,
                c.NrMinuteReconectare,
                c.DestinatieAmef,
                c.URLAmef
            ))
            .ToListAsync()
    };


    return Ok(new { Message = "Login successful", User = userDto });
}


        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserWithCaseDeMarcat(string userId)
        {
            var user = await _context.Users
                .Include(u => u.CaseDeMarcat)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out successfully.");
        }
    }

    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
