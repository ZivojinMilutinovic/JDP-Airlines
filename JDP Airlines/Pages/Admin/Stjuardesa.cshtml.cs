using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDP_Airlines.Pages.Admin
{
    public class StjuardesaModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Stjuardesa Stjuardesa { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Stjuardesa> ListaStjuardesa { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Stjuardesa = await DataProvider.getStjuardesa(id);
            ListaStjuardesa = await DataProvider.getSveStjuardese();
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {

            var stjuardesa = await DataProvider.createStjuardesa(Stjuardesa);
            Korisnik korisnik = new Korisnik
            {
                UserName = stjuardesa.Ime + stjuardesa.Prezime,
                Password = stjuardesa.Ime + stjuardesa.Prezime
            };
            korisnik = await DataProvider.createKorisnik(korisnik);
            await DataProvider.createStjuardesaKorisnik(stjuardesa.Id, korisnik.Id);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            await DataProvider.updateStjuardesa(Stjuardesa);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await DataProvider.deleteStjuardesa(id);
            return RedirectToPage();
        }
    }
}
