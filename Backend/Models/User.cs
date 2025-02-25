using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
    public class User : IdentityUser
    {
        public List<CasaDeMarcat?> CaseDeMarcat { get; set; } = new List<CasaDeMarcat?>();
    }
}
