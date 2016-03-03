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

            DateTime startdatum = new DateTime();
            if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
            {
                if (DateTime.Now.TimeOfDay.Hours < 17 /*Convert.ToDateTime("05:00:00 PM")*/)
                {
                    startdatum = GetNextWeekday(DateTime.Today, DayOfWeek.Monday);
                }
                else
                {
                    startdatum = GetNextWeekday(DateTime.Today.AddDays(7), DayOfWeek.Monday);
                }
            }
            else if (DateTime.Now.DayOfWeek < DayOfWeek.Friday)
            {
                startdatum = GetNextWeekday(DateTime.Today.AddDays(1), DayOfWeek.Monday);
            }
            else
            {
                startdatum = GetNextWeekday(DateTime.Today.AddDays(7), DayOfWeek.Monday);
            }

            if (gebruiker != null) {
            if (gebruiker.Verlanglijst.Materialen.Count == 0)
            {
                return View("LegeLijst");
            }
        }
        ViewBag.Startdatum = startdatum;
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