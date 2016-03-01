using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projecten2.NET.Models;
using Projecten2.NET.Models.DAL;
using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.ViewModels;

namespace Projecten2.NET.Controllers
{
    public class VerlanglijstjeController : Controller
    {
        private IMateriaalRepository materiaalRepository;
        private CatalogusContext db = new CatalogusContext();

        public ActionResult Index()
        {
            var lijst = Verlanglijstje.GetVerlanglijstje(this.HttpContext);
            var viewModel = new VerlanglijstjeViewModel
            {
                Items = lijst.GetVerlanglijstjeItems(),
                Totaal = lijst.GetTotaal()
            };
            return View(viewModel);
        }

        public ActionResult AddToVerlanglijstje(int id)
        {
            // materiaal uit db halen.
            var addedMateriaal = db.Materialen.Single(item => item.MateriaalId == id);
            //in verlanglijstje stoppen
            var lijst = Verlanglijstje.GetVerlanglijstje(this.HttpContext);
            lijst.AddToVerlanglijstje(addedMateriaal);
            
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromVerlanglijst(int id)
        {
            //verwijdering van item
            var lijst = Verlanglijstje.GetVerlanglijstje(this.HttpContext);
            //Confirmation verwijdering
            string naam = db.Lijst.Single(item => item.MateriaalId == id).Materiaal.Artikelnaam;
            int itemAantal = lijst.RemoveFromVerlanglijstje(id);


            var viewRemove = new VerlanglijstjeRemoveViewModel
            {
                Bericht = Server.HtmlEncode(naam) + " is succesvol verwijderd van uw verlanglijstje.",
                ItemAantal = itemAantal,
                LijstTotaal = lijst.GetTotaal(),
                LijstAantal = lijst.GetAantal()
               
            };

            return Json(viewRemove);

        }

        // GET: /Verlanglijstje/Overzicht
        [ChildActionOnly]
        public ActionResult Overzicht()
        {
            var lijst = Verlanglijstje.GetVerlanglijstje(this.HttpContext);

            ViewData["LijstAantal"] = lijst.GetAantal();
            return PartialView("Overzicht");
        }
    }
}