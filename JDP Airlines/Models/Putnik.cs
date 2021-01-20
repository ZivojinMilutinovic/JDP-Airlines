using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDP_Airlines.Models
{
    public class Putnik
    {
        public string BrojPasosa { get; set; }
        public string IdLeta_IdPrtljaga { get; set; }
        public string GetNaziv() { return $"{BrojPasosa}:{Ime} {Prezime}"; }
        public string Email { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public List<Let> Letovi { get; set; }
        public int BrojBodova { get; set; }     //za popuste
    }
}
