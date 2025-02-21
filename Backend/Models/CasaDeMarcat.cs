using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("CasaDeMarcat")]
    public class CasaDeMarcat
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public string? NUI { get; set; } // Inițial null
        public int? ProfileType { get; set; } // Inițial null
        public string? ProfileReset { get; set; } 
        public DateTime? Date { get; set; } 
        public int? ReconnectMinutes { get; set; } 
        public string? DestinationAMEF { get; set; } 
        public string? UrlAMEF { get; set; } 

        public CasaDeMarcat() { }

        public CasaDeMarcat(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public CasaDeMarcat(string nui, int profileType, string? profileReset, DateTime? date, int? reconnectMinutes, string? destinationAMEF, string? urlAMEF)
        {
            NUI = nui;
            ProfileType = profileType;
            ProfileReset = profileReset;
            Date = date;
            ReconnectMinutes = reconnectMinutes;
            DestinationAMEF = destinationAMEF;
            UrlAMEF = urlAMEF;
        }
    }
}
