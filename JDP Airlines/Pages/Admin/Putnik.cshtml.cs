using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDP_Airlines.Pages.Admin
{
    public class PutnikModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Putnik Putnik { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Putnik> ListaPutnika { get; set; }

        public async Task OnGetAsync(string brPasosa)
        {
            Putnik = await DataProvider.getPutnik(brPasosa);
            ListaPutnika = await DataProvider.getSvePutnike();
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            await DataProvider.createPutnik(Putnik);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            await DataProvider.updatePutnik(Putnik);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(string brPasosa)
        {
            await DataProvider.deletePutnik(brPasosa);
            return RedirectToPage();
        }
    }
}