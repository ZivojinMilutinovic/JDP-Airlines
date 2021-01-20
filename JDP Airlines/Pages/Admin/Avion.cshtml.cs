using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDP_Airlines.Pages.Admin
{
    public class AvionModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Avion Avion { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Avion> ListaAviona { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Avion = await DataProvider.getAvion(id);
            ListaAviona = await DataProvider.getSveAvione();
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            await DataProvider.createAvion(Avion);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            await DataProvider.updateAvion(Avion);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await DataProvider.deleteAvion(id);
            return RedirectToPage();
        }
    }
}