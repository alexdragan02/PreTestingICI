using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class CasaDeMarcat
    {
        public int Id { get; set; } // ID unic pentru baza de date
        public string Name { get; set; } = string.Empty;

        public string? NUI { get; set; } = string.Empty;

        public int TipProfil { get; set; } = 0; // Implicit TipProfil = 0
        public bool? TipReset { get; set; } = false; // Implicit TipReset = false

        public DateTime? DateTime { get; set; } // Apare doar dacă TipProfil = 1 și TipReset = true
        public int? NrMinuteReconectare { get; set; }
        public string? DestinatieAmef { get; set; }
        public string? URLAmef { get; set; }

        // 🔹 Adăugăm relația cu `User`
        [Required]
        public string UserId { get; set; } = string.Empty; // Foreign Key
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        // ✅ Constructor pentru inițializare corectă
        public CasaDeMarcat()
        {
            if (TipProfil == 0)
            {
                TipReset = false;
            }
        }
    }
}
