using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDP_Airlines.Pages
{
    public class ZaposleniModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string[] ListaEntiteta { get; set; }
        [BindProperty(SupportsGet = true)]
        public string[] ListaOpisa { get; set; }
        [BindProperty(SupportsGet = true)]
        public string[] ListaSlika { get; set; }
        [BindProperty(SupportsGet = true)]
        public string[] LinkoviZaStranice { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Let> Letovi { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Let> KargoLetovi { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Putnik> Putnici { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<RobaKargo> RobaKargo{ get; set; }
        [BindProperty]
        public List<Guid> LetoviGuid { get; set; }
        [BindProperty]
        public List<Guid> KargoLetoviGuid { get; set; }
        [BindProperty]
        public List<string> PutniciPasosi { get; set; }
        [BindProperty]
        public List<Guid> KargoGuid { get; set; }
        [BindProperty]
        public string BrojPasosa { get; set; }
        [BindProperty]
        public Putnik Putnik { get; set; }
        [BindProperty]
        public Let Let { get; set; }
        [BindProperty]
        public Aerodrom AerodromSa { get; set; }
        [BindProperty]
        public Aerodrom AerodromNa { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            await PribaviInfo();
        }
        public async Task<IActionResult> OnPostPutnikAsync()
        {
            foreach (var letId in LetoviGuid)
            {
                foreach (var brojPasosa in PutniciPasosi)
                {
                    await DataProvider.createLetPutnik(letId, brojPasosa);
                }
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostKargoAsync()
        {
            foreach (var letId in KargoLetoviGuid)
            {
                foreach (var kargoId in KargoGuid)
                {
                    await DataProvider.createLetRobaKargo(letId,kargoId);
                }
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostPrtljagAsync()
        {
            await PribaviInfo();
            Putnik = await DataProvider.getPutnik(BrojPasosa);
            if (Putnik.IdLeta_IdPrtljaga.Length == 0)
            {
                ErrorMessage = "Ne postoji prtljag za ovog putnika";
                return Page();
            }
            Guid letId=new Guid(Putnik.IdLeta_IdPrtljaga.Split("::")[0]);
            Let = await DataProvider.getLet(letId);
            AerodromSa=await DataProvider.getLetAerodrom(letId, true);
            AerodromNa = await DataProvider.getLetAerodrom(letId, false);
            return Page();
        }
        public async Task PribaviInfo()
        {
            ListaEntiteta = new string[] { "Let", "Putnik", "Kargo" };
            ListaOpisa = new string[] {
                "Upravljajte sa velikom lakocom svim vrstama letova na razlicitm lokacijama",
                "Uprvaljajte putnicima i njihovim podacima",
                "Upravljajte robom koja ce se prevoziti na odredjenom letu",
  };

            ListaSlika = new string[] {
                "https://www.researchgate.net/profile/Muhammet_Deveci/publication/341042975/figure/fig2/AS:885942185365505@1588236485608/Turkish-Airlines-route-map.jpg",

                "https://i.insider.com/5b311c575e48ec1b008b45ed?width=1100&format=jpeg&auto=webp",
                "https://www.shiplilly.com/wp-content/uploads/2013/07/Air-cargo.jpg",
};
            LinkoviZaStranice = ListaEntiteta;
            Letovi = await DataProvider.getSveLetove();
            KargoLetovi = Letovi.Where(x => x.Putnicki == false).ToList();
            for (int i = 0; i < Letovi.Count; i++)
            {
                Letovi[i].LetiNa = await DataProvider.getLetAerodrom(Letovi[i].ID, false);
                Letovi[i].LetiSa = await DataProvider.getLetAerodrom(Letovi[i].ID, true);
            }
            for (int i = 0; i < KargoLetovi.Count; i++)
            {
                KargoLetovi[i].LetiNa = await DataProvider.getLetAerodrom(KargoLetovi[i].ID, false);
                KargoLetovi[i].LetiSa = await DataProvider.getLetAerodrom(KargoLetovi[i].ID, true);
            }
            Putnici = await DataProvider.getSvePutnike();
            RobaKargo = await DataProvider.getSveRobaKargo();
            
        }
    }
}

