using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDP_Airlines.Pages.Admin
{
    public class KargoModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public RobaKargo Kargo { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<RobaKargo> ListaRobaKargo { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            Kargo = await DataProvider.getRobaKargo(id);
            ListaRobaKargo = await DataProvider.getSveRobaKargo();
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            await DataProvider.createRobaKargo(Kargo);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            await DataProvider.updateRobaKargo(Kargo);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await DataProvider.deleteRobaKargo(id);
            return RedirectToPage();
        }
    }
}