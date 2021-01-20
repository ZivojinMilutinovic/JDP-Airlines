using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDP_Airlines.Models
{
    public class Aerodrom
    {
        public Guid Id { get; set; }
        public string Oznaka { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }
        public string Naziv { get; set; }
        public int BrojPisti { get; set; }
        
        public List<Let> OdlazniLetovi { get; set; }
        public List<Let> DolazniLetovi { get; set; }
    }
}
