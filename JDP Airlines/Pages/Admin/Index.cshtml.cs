using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDP_Airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JDP_Airlines.Pages.Admin
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string[] ListaEntiteta { get; set; }
        [BindProperty(SupportsGet = true)]
        public string[] ListaOpisa { get; set; }
        [BindProperty(SupportsGet = true)]
        public string[] ListaSlika { get; set; }
        [BindProperty(SupportsGet = true)]
        public string[] LinkoviZaStranice { get; set; }
       
        public void OnGet()
        {
            ListaEntiteta = new string[] { "Aerdrom",
            "Avion","Korisnik","Pilot","Saradnicka Kompanija",
            "Stjuardesa"};
            ListaOpisa = new string[] { "Upravljajte svi aerdromima na razlicitim lokacijama u svetu",
            "Upravljajte razlicitim vrstama predvidjene za sve svrhe letova ",
                "Upravljajte svim korisnicima u aplikaciji ,dodajte i brisite naloge",
                "Upravljajte pilotima",
                "Upravljajte saradnicim kompanijama i njihovim uslugama",
            "Upravljajte stjuardesama"};

            ListaSlika = new string[] { "https://blog.aci.aero/wp-content/uploads/2019/03/shutterstock_745544935-952x635.jpg",
            "https://scx2.b-cdn.net/gfx/news/2019/toomanyairpl.jpg",
                "https://image.freepik.com/free-photo/woman-customer-service-worker-call-center-smiling-operator_200074-19.jpg",
                "https://www.thebalancecareers.com/thmb/OvN4y4eK89Sf7g93-C-qQg47WEs=/1182x888/filters:fill(auto,1)/GettyImages-138311473-1--56a058c65f9b58eba4affd12.jpg",
                "https://marketresearch.biz/wp-content/uploads/2018/12/inflight-catering-market.jpg",
            "https://nypost.com/wp-content/uploads/sites/2/2017/10/171027-flight-attendant-jobs-feature.jpg?quality=80&strip=all"};
            LinkoviZaStranice = new string[] {"Aerdrom",
            "Avion","Korisnik","Pilot","SaradnickaKompanija",
            "Stjuardesa" };
            
        }
    }
}
