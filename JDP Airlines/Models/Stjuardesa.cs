using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDP_Airlines.Models
{
    public class Stjuardesa
    {
        public Guid Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int GodineStaza { get; set; }
        public List<Let> Letovi { get; set; }
        public Stjuardesa()
        {
            Letovi = new List<Let>();
        }
    }
}
