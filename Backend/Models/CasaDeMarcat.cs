using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Models.XML;

namespace Backend.Models
{
    public class CasaDeMarcat
    {
        public int Id { get; set; } // ID unic pentru baza de date
        public string Name { get; set; } = string.Empty;

        public string? NUI { get; set; } = string.Empty;

        public int TipProfil { get; set; } // Implicit TipProfil = 0
        public bool TipReset { get; set; } // Implicit TipReset = false

        public DateTime? DateTime { get; set; } // Apare doar dacă TipProfil = 1 și TipReset = true
        public int? NrMinuteReconectare { get; set; }
        public string? DestinatieAmef { get; set; }
        public string? URLAmef { get; set; }

        public virtual List<Msj> MesajXML { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty; // Foreign Key

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        // ✅ Constructor pentru inițializare corectă
        public CasaDeMarcat() { }

        public CasaDeMarcat(
            int id,
            string name,
            string? nui,
            int tipProfil,
            bool tipReset,
            DateTime? dateTime,
            int? nrMinuteReconectare,
            string? destinatieAmef,
            string? urlAmef,
            string userId,
            User user,
            List<Msj> mesajeXML
        )
        {
            this.Id = id;
            this.Name = name;
            this.NUI = nui;
            this.TipProfil = tipProfil;
            this.TipReset = tipReset;
            this.DateTime = dateTime;
            this.NrMinuteReconectare = nrMinuteReconectare;
            this.DestinatieAmef = destinatieAmef;
            this.URLAmef = urlAmef;
            this.UserId = userId;
            this.User = user;
            this.MesajXML = mesajeXML;
        }

        // Constructor de copiere
        public CasaDeMarcat(CasaDeMarcat casa)
        {
            this.Id = casa.Id;
            this.Name = casa.Name;
            this.NUI = casa.NUI;
            this.TipProfil = casa.TipProfil;
            this.TipReset = casa.TipReset;
            this.DateTime = casa.DateTime;
            this.NrMinuteReconectare = casa.NrMinuteReconectare;
            this.DestinatieAmef = casa.DestinatieAmef;
            this.URLAmef = casa.URLAmef;
            this.UserId = casa.UserId;
            this.User = casa.User;
            this.MesajXML = casa.MesajXML;

        }

        // Constructor de instanțiere cu toate elementele
    }
}
