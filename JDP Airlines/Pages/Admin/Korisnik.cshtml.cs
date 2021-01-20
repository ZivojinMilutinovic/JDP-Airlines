using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDP_Airlines.Pages.Admin
{
    public class KorisnikModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Korisnik Korisnik { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Korisnik> ListaKorisnika { get; set; }

        public async Task OnGetAsync(string username)
        {
            if (username != null)
                Korisnik = await DataProvider.getKorisnikUsername(username);
            else
                Korisnik = new Korisnik();
            ListaKorisnika = await DataProvider.getSveKorisnike();
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            var korisnik = await DataProvider.getKorisnik(Korisnik.Id);
            if (korisnik.Id != Guid.Empty)
            {
                return RedirectToPage();
            }
            if (Korisnik.UserName == null)
            {
                ErrorMessage = "Username je obavezno polje";
                return null;
            }
            else if (Korisnik.Password == null)
            {
                ErrorMessage = "Password je obavezno polje";
                return null;
            }
            await DataProvider.createKorisnik(Korisnik);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            await DataProvider.updateKorisnik(Korisnik);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await DataProvider.deleteKorisnik(id);
            return RedirectToPage();
        }

        public async Task<bool> MoguceBrisanje(Guid id)
        {
            if (await DataProvider.daLiJeKorisnikPilotIliStjuardesa(id) == 3)
                return true;
            else return false;
        }
    }
}