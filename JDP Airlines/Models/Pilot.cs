using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDP_Airlines.Models
{
    public class Pilot
    {
        
        public Guid Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int GodineStaza  { get; set; }
        public int Starost { get; set; }
        public List<Let> Upravlja { get; set; }
        public List<Let> Kopilotira { get; set; }

        public Pilot()
        {
            Upravlja = new List<Let>();
            Kopilotira = new List<Let>();
        }
    }
}
