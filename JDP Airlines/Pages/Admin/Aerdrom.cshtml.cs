using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDP_Airlines.Pages.Admin
{
    public class AerdromModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public Aerodrom Aerdrom { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        [BindProperty(SupportsGet =true)]
        public List<Aerodrom> ListaAerdrom { get; set; }
        
        public async Task OnGetAsync(Guid id)
        {
            Aerdrom = await DataProvider.getAerdrom(id);
            ListaAerdrom = await DataProvider.getSveAerdrome();
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            var aerderom = await DataProvider.getAerdromOznaka(Aerdrom.Oznaka);
            if (aerderom != null)
            {
                return RedirectToPage();
            }
            await DataProvider.createAerdrom(Aerdrom);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            await DataProvider.updateAerdrom(Aerdrom);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await DataProvider.deleteAerdrom(id);
            return RedirectToPage();
        }
    }
}
