using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JDP_Airlines.Pages
{
    public class PilotModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public Pilot Pilot { get; set; }
        public List<SaradnickaKompanija> Kompanije { get; set; }
        public List<Let> ListaLetova = new List<Let>();
        [BindProperty]
        public Guid IzabranLet { get; set; }
        public Let LetDetalji { get; set; }
        public async Task OnGetAsync(Guid id)
        {

            Pilot = await DataProvider.GetPilotKorisnik(id);
            Kompanije = new List<SaradnickaKompanija>();
            foreach (var let in Pilot.Upravlja)
            {
                Kompanije.AddRange(let.SaradnickeKompanije);
                ListaLetova.Add(let);
            }
            foreach (var let in Pilot.Kopilotira)
            {
                Kompanije.AddRange(let.SaradnickeKompanije);
                ListaLetova.Add(let);
            }
            LetDetalji = ListaLetova.FirstOrDefault();
        }

        public async Task<IActionResult> OnPostOsveziAsync(Guid id)
        {
            var idKorisnika = await DataProvider.getKorisnikPilot(id);
            Pilot = await DataProvider.GetPilotKorisnik(idKorisnika);
            Kompanije = new List<SaradnickaKompanija>();
            foreach (var let in Pilot.Upravlja)
            {
                Kompanije.AddRange(let.SaradnickeKompanije);
                ListaLetova.Add(let);
            }
            foreach (var let in Pilot.Kopilotira)
            {
                Kompanije.AddRange(let.SaradnickeKompanije);
                ListaLetova.Add(let);
            }
            LetDetalji = ListaLetova.FirstOrDefault(r => r.ID == IzabranLet);
            return Page();
        }
    }
}
