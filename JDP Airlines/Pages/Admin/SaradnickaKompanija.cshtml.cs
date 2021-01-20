using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDP_Airlines.Pages.Admin
{
    public class SaradnickaKompanijaModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public SaradnickaKompanija SaradnickaKompanija { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SaradnickaKompanija> ListaSaradnickaKompanija { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            SaradnickaKompanija = await DataProvider.getSaradnickaKompanija(id);
            ListaSaradnickaKompanija = await DataProvider.getSveSaradnickeKompanije();
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            
            await DataProvider.createSaradnickaKompanija(SaradnickaKompanija);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            await DataProvider.updateSaradnickaKompanija(SaradnickaKompanija);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await DataProvider.deleteSaradnickaKompanija(id);
            return RedirectToPage();
        }
    }
}
