using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDP_Airlines.Pages
{
    public class StjuardesaModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Stjuardesa Stjuardesa { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        [BindProperty]
        public Dictionary<Let, int> LetoviPresedanja { get; set; }   //drugi parametar je redni broj presedanja
        [BindProperty]
        public Dictionary<Stjuardesa, int> Kolege { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Stjuardesa = await DataProvider.getStjuardesaKorisnik(id);
            Kolege = new Dictionary<Stjuardesa, int>();
            foreach (var let in Stjuardesa.Letovi)
            {
                foreach (var stjuardesa in let.Stjuardese)
                {
                    if (stjuardesa.Id == Stjuardesa.Id)
                        continue;
                    if (Kolege.Keys.FirstOrDefault(x => x.Id == stjuardesa.Id) == null)
                    {
                        Kolege[stjuardesa] = 1;
                    }
                    else
                    {
                        Kolege[Kolege.Keys.FirstOrDefault(x => x.Id == stjuardesa.Id)]++;
                    }
                }
            }
            LetoviPresedanja = new Dictionary<Let, int>();
            int i = 1;
            foreach (var let in Stjuardesa.Letovi.OrderBy(x => x.VremePoletanja))
            {
                if (LetoviPresedanja.Count == 0)
                {
                    LetoviPresedanja[let] = i;
                }
                else
                {
                    if (let.VremePoletanja.Subtract(LetoviPresedanja.Last().Key.VremeSletanja) <= new TimeSpan(24, 0, 0)
                        && let.LetiSa.Id == LetoviPresedanja.Last().Key.LetiNa.Id)
                    {
                        LetoviPresedanja[let] = i;
                    }
                    else
                    {
                        LetoviPresedanja[let] = ++i;
                    }
                }
            }
        }
    }
}
