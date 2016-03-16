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

        [HttpPost]
        public ActionResult AddToVerlanglijst(string naam, Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                {
                    Materiaal m = materiaalRepository.FindByArtikelNaam(naam);
                    try
                    {
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
                    catch (Exception)
                    {
                        TempData["error"] = $"Materiaal kan niet toegevoegd worden aan uw verlanglijst";
                    }
                }

            }
            return RedirectToAction("Index", "Catalogus");
        }


        [HttpPost]
        public ActionResult RemoveFromVerlanglijst(string naam, Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Materiaal m = materiaalRepository.FindByArtikelNaam(naam);
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

        [HttpPost]
        public ActionResult RemoveFromVerlanglijstInCato(string naam, Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                Materiaal m = materiaalRepository.FindByArtikelNaam(naam);
                gebruiker.RemoveMateriaalFromVerlanglijst(m);
                gebruikersRepository.SaveChanges();
                if (!gebruiker.BezitVerlanglijstMateriaal(m))
                    TempData["info"] = $"Materiaal " + m.Artikelnaam + " is uit de verlanglijst verwijderd!";
                /*}
                // catch (Exception e)
                 { 
                     throw new Exception(e.Message);
                 }*/
            }
            return RedirectToAction("Index", "Catalogus");

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

        [ChildActionOnly]
        public ActionResult Overzicht(Gebruiker gebruiker)
        {
            if (gebruiker.Verlanglijst.Materialen.Count!=0)
            {
                ViewData["Teller"] = gebruiker.Verlanglijst.Materialen.Count;
            }
            return PartialView("Overzicht");
        }
    }
}