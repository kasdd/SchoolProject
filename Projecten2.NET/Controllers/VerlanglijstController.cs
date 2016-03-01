using System.Web.Mvc;
using Projecten2.NET.Models.DAL;
using Projecten2.NET.Models.Domain;


namespace Projecten2.NET.Controllers
{
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

        public ActionResult AddToVerlanglijst(string naam, Gebruiker gebruiker)
        {
            Materiaal m = materiaalRepository.FindByName(naam);
            if (m != null)
            {
                if (gebruiker.Verlanglijst.Materialen.Contains(m))
                    TempData["Info"] = "Materiaal " + m.Artikelnaam + " zit al in je verlanglijst!";
                else
                {
                    gebruiker.Verlanglijst.Materialen.Add(m);
                    gebruikerRepository.SaveChanges();
                }
                    

            }
            return RedirectToAction("Index", "Verlanglijst");
        }

        public ActionResult RemoveFromVerlanglijst(string naam, Gebruiker gebruiker)
        {
            Materiaal m = materiaalRepository.FindByName(naam);
            gebruiker.Verlanglijst.Materialen.Remove(m);
            gebruikerRepository.SaveChanges();
            return RedirectToAction("Index", "Verlanglijst");

        }
    }
}