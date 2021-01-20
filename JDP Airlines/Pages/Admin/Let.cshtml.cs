using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JDP_Airlines.Pages.Admin
{
    public class LetModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Let Let { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Let> ListaLetova { get; set; }

        #region Liste
        [BindProperty(SupportsGet = true)]
        public List<Avion> Avioni { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Aerodrom> Aerodromi { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Pilot> Piloti { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SaradnickaKompanija> Kompanije { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Stjuardesa> Stjuardese { get; set; }
        #endregion

        #region PomocneListe
        [BindProperty(SupportsGet = true)]
        public List<Guid> OdabraneKompanije { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Guid> OdabraneStjuardese { get; set; }
        #endregion

        public async Task OnGetAsync(Guid id)
        {
            Let = await DataProvider.getLet(id);
            await Ucitaj(id);
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            var let = await DataProvider.getLet(Let.ID);
            await Ucitaj(let.ID);
            Let.Avion = await DataProvider.getAvion(Let.Avion.ID);
            if (let.ID != Guid.Empty)
            {
                return RedirectToPage();
            }
            if (Let.VremePoletanja >= Let.VremeSletanja)
            {
                ErrorMessage = "Greska! Vreme sletanja je pre vremena poletanja!";
                return null;
            }
            else if (Let.Avion.ID == Guid.Empty)
            {
                ErrorMessage = "Nije izabran avion!";
                return null;
            }
            else if (Let.Avion.Putnicki != Let.Putnicki)
            {
                ErrorMessage = "Pogresan tip aviona!";
                return null;
            }
            else if (Let.LetiNa.Id == Guid.Empty)
            {
                ErrorMessage = "Nije odabran aerodrom na koji slece!";
                return null;
            }
            else if (Let.LetiSa.Id == Guid.Empty)
            {
                ErrorMessage = "Nije odabran aerodrom sa kojeg polece!";
                return null;
            }
            else if (Let.LetiNa == Let.LetiSa)
            {
                ErrorMessage = "Aerodromi su isti!";
                return null;
            }
            else if (Let.Pilot.Id == Guid.Empty)
            {
                ErrorMessage = "Nije odabran pilot!";
                return null;
            }
            else if (Let.Kopilot.Id == Guid.Empty)
            {
                ErrorMessage = "Nije odabran kopilot!";
                return null;
            }
            else if (Let.Pilot == Let.Kopilot)
            {
                ErrorMessage = "Pilot i kopilot moraju biti razliciti!";
                return null;
            }

            let = await DataProvider.createLet(Let);
            await DataProvider.createLetAerodromi(let.ID, Let.LetiSa.Id, Let.LetiNa.Id);
            await DataProvider.createLetAvion(let.ID, Let.Avion.ID);
            await DataProvider.createLetPiloti(let.ID, Let.Pilot.Id, Let.Kopilot.Id);
            await DataProvider.createLetStjuardese(let.ID, OdabraneStjuardese);
            await DataProvider.createLetSaradnickeKompanije(let.ID, OdabraneKompanije);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdateAsync(Guid id)
        {
            //await Ucitaj(id);
            await DataProvider.updateLet(Let, OdabraneStjuardese, OdabraneKompanije);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            //await Ucitaj(id);
            await DataProvider.deleteLet(id);
            return RedirectToPage();
        }

        public async Task Ucitaj(Guid id)
        {
            if (Let.ID != Guid.Empty)
            {
                Let.LetiSa = await DataProvider.getLetAerodrom(Let.ID, true);
                Let.LetiNa = await DataProvider.getLetAerodrom(Let.ID, false);
                Let.Avion = await DataProvider.getLetAvion(Let.ID);
                Let.Pilot = await DataProvider.getLetPilot(Let.ID, true);
                Let.Kopilot = await DataProvider.getLetPilot(Let.ID, false);
                OdabraneStjuardese = await DataProvider.getLetStjuardese(Let.ID);
                OdabraneKompanije = await DataProvider.getLetSaradnickeKompanije(Let.ID);
            }
            ListaLetova = await DataProvider.getSveLetove();
            Aerodromi = await DataProvider.getSveAerdrome();
            Avioni = await DataProvider.getSveAvione();
            Piloti = await DataProvider.getSvePilote();
            Kompanije = await DataProvider.getSveSaradnickeKompanije();
            Stjuardese = await DataProvider.getSveStjuardese();
        }
    }
}