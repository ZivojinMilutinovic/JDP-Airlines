using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDP_Airlines.Models
{
    public class RobaKargo
    {
        public Guid ID { get; set; }
        public string TipRoba { get; set; }
        public double Tezina { get; set; }
        public double Sirina { get; set; }
        public double Duzina { get; set; }
        public double Visina { get; set; }
        public Let Let { get; set; }
    }
}
