using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;

namespace JDP_Airlines.Pages
{
    public class IndexModel : PageModel
    {
      
        private readonly ILogger<IndexModel> _logger;
        public string result;

        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string ErrorMessageKorisnik { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            
        }

        public  void OnGet()
        {
        }

        public async Task<IActionResult> OnPostKorisnikAsync()
        {
            Korisnik korisnik = await DataProvider.getKorisnikUsername(Username);
            int p = await DataProvider.daLiJeKorisnikPilotIliStjuardesa(korisnik.Id);
            if (korisnik.Password != Password)
            {
                ErrorMessageKorisnik = "Sifre nisu jednake";
                return Page();
            }
            else if (p == -1)
            {
                ErrorMessageKorisnik = "Ne postoji korisnik sa tim usernameom";
                return Page();
            }
            if (p == 0)
            {
                return RedirectToPage("/Admin/Index");
            }
            else if (p == 1)
            {
                return RedirectToPage("/Pilot", new { id = korisnik.Id }); 
            }
            else if (p == 2)
            {
                return Redirect("/Stjuardesa?id="+korisnik.Id);
            }
            else if (p == 3)
            {
                return RedirectToPage("/Zaposleni");
            }
            return RedirectToPage();
        }
    }
}
