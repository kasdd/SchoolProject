using System.Web.Mvc;
using Projecten2.NET.Models.DAL;
using Projecten2.NET.Models.Domain;
using System;

namespace Projecten2.NET.Controllers
{
    [Authorize]
    public class VerlanglijstController : Controller
    {
        private IMateriaalRepository materiaalRepository;
        private IGebruikerRepository gebruikersRepository;

        public VerlanglijstController(IMateriaalRepository materiaalRepository, IGebruikerRepository gebruikerRepository)
        {
            this.materiaalRepository = materiaalRepository;
            this.gebruikersRepository = gebruikerRepository;
        }

        public ActionResult Index(Gebruiker gebruiker)
        {
            if (gebruiker?.Verlanglijst.Materialen.Count == 0)
            {
                return View("LegeLijst");
            }
            ViewBag.Total = gebruiker.Verlanglijst.Materialen.Count;
            return View(gebruiker.Verlanglijst.Materialen);

        }

        public ActionResult AddToVerlanglijst(string nummer, Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                Materiaal m = materiaalRepository.FindByArtikelNr(nummer);
                if (gebruiker.BezitVerlanglijstMateriaal(m))
                    TempData["error"] = $"Materiaal " + m.Artikelnaam + " zit al in uw verlanglijst!";
                else
                {
                    gebruiker.AddMateriaalToVerlanglijst(m);
                    gebruikersRepository.SaveChanges();
                    if (gebruiker.BezitVerlanglijstMateriaal(m))
                        TempData["info"] = $"Materiaal " + m.Artikelnaam + " is aan uw verlanglijst toegevoegd!";
                }
            }
            return RedirectToAction("Index", "Catalogus");
        }

        public ActionResult RemoveFromVerlanglijst(string nummer, Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Materiaal m = materiaalRepository.FindByArtikelNr(nummer);
                    gebruiker.RemoveMateriaalFromVerlanglijst(m);
                    gebruikersRepository.SaveChanges();
                    if (!gebruiker.BezitVerlanglijstMateriaal(m))
                        TempData["info"] = $"Materiaal " + m.Artikelnaam + " is uit de verlanglijst verwijderd!";
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return RedirectToAction("Index", "Verlanglijst");

        }

        //hulpklasses
        public static DateTime GetNextWeekday(DateTime vandaag, DayOfWeek verwachteDag)
        {
            int daysToAdd = ((int)verwachteDag - (int)vandaag.DayOfWeek + 7) % 7;
            return vandaag.AddDays(daysToAdd);
        }

        private int getAantal(int selectedValue = 1)
        {
            return selectedValue;
        }
    }
}