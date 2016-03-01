using System.Web.Mvc;
using Projecten2.NET.Models.DAL;
using Projecten2.NET.Models.Domain;


namespace Projecten2.NET.Controllers
{
    [Authorize]
    public class VerlanglijstController : Controller
    {
        private IMateriaalRepository materiaalRepository;
        private IGebruikerRepository gebruikerRepository;

        public VerlanglijstController(IMateriaalRepository materiaalRepository, IGebruikerRepository gebruikerRepository)
        {
            this.materiaalRepository = materiaalRepository;
            this.gebruikerRepository = gebruikerRepository;
        }

        public ActionResult Index(Gebruiker gebruiker)
        {
            if (gebruiker.Verlanglijst.Materialen.Count == 0)
            {
                return View("LegeLijst");
            }
            ViewBag.Total = gebruiker.Verlanglijst.Materialen.Count;
            return View(gebruiker.Verlanglijst.Materialen);

        }

        public ActionResult AddToVerlanglijst(string nummer, Gebruiker gebruiker)
        {
            Materiaal m = materiaalRepository.FindByArtikelNr(nummer);
            if (m != null)
            {
                if (gebruiker.Verlanglijst.Materialen.Contains(m))
                    TempData["Info"] = "Materiaal " + m.Artikelnaam + " zit al in uw verlanglijst!";
                else
                {
                    gebruiker.Verlanglijst.Materialen.Add(m);
                    //gebruikerRepository.SaveChanges();
                    if (gebruiker.Verlanglijst.Materialen.Contains(m))
                        TempData["Info"] = "Materiaal " + m.Artikelnaam + " is aan uw verlanglijst toegevoegd!";
                }
            }
            return RedirectToAction("Index", "Catalogus");
        }

        public ActionResult RemoveFromVerlanglijst(string nummer, Gebruiker gebruiker)
        {
            Materiaal m = materiaalRepository.FindByArtikelNr(nummer);
            gebruiker.Verlanglijst.Materialen.Remove(m);
            if (!gebruiker.Verlanglijst.Materialen.Contains(m))
                TempData["Info"] = "Materiaal " + m.Artikelnaam + " is uit de verlanglijst verwijderd!";
            //gebruikerRepository.SaveChanges();
            return RedirectToAction("Index", "Verlanglijst");

        }
    }
}