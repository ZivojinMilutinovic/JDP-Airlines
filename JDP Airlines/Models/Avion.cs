using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDP_Airlines.Models
{
    public class Avion
    {
        public Guid ID { get; set; }
        public string Proizvodjac { get; set; }
        public bool Putnicki { get; set; }
        public double? Nosivost { get; set; }    //tezina
        public int? Kapacitet { get; set; }      //broj putnika
        public double? Sirina { get; set; }
        public double? Duzina { get; set; }
        public double? Visina { get; set; }
        public int GodinaProizvodnje { get; set; }
        public List<Let> Letovi { get; set; }
    }
}
