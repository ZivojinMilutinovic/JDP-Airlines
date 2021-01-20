using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDP_Airlines.Pages.Admin
{
    public class PilotModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Pilot Pilot { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Pilot> ListaPilota { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            if (id != null)
                Pilot = await DataProvider.getPilot(id);
            else
                Pilot = new Pilot();
            ListaPilota = await DataProvider.getSvePilote();
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            var pilot = await DataProvider.getPilot(Pilot.Id);
            if (pilot.Id != Guid.Empty)
            {
                return RedirectToPage();
            }
            pilot = await DataProvider.createPilot(Pilot);
            Korisnik korisnik = new Korisnik
            {
                UserName = Pilot.Ime + Pilot.Prezime,
                Password = Pilot.Ime + Pilot.Prezime
            };
            korisnik = await DataProvider.createKorisnik(korisnik);
            await DataProvider.createPilotKorisnik(pilot.Id, korisnik.Id);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            await DataProvider.updatePilot(Pilot);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await DataProvider.deletePilot(id);
            return RedirectToPage();
        }
    }
}