using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDP_Airlines.Models
{
    public class SaradnickaKompanija
    {
        public Guid Id { get; set; }
        public string Naziv { get; set; }
        public string TipSaradnje { get; set; }
        public int CenaUsluge { get; set; }
        public List<Let> Letovi { get; set; }
    }
}
