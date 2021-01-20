using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDP_Airlines.Models
{
    public class Let
    {
        public Guid ID { get; set; }
        public DateTime VremePoletanja { get; set; }
        public DateTime VremeSletanja { get; set; }
        public string GetNaziv() { return $"{LetiSa.Naziv}-{LetiNa.Naziv}:{VremePoletanja}-{VremePoletanja}"; }
        public bool Putnicki { get; set; }
        public Avion Avion { get; set; }
        public Aerodrom LetiSa { get; set; }
        public Aerodrom LetiNa { get; set; }
        public Pilot Pilot { get; set; }
        public Pilot Kopilot { get; set; }
        public List<SaradnickaKompanija> SaradnickeKompanije { get; set; }
        public List<RobaKargo> Roba { get; set; }
        public List<Putnik> Putnici { get; set; }
        public List<Stjuardesa> Stjuardese { get; set; }
    }
}
