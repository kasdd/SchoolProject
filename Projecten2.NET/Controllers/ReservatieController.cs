using Projecten2.NET.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projecten2.NET.Controllers
{
    [Authorize]
    public class ReservatieController : Controller
    {

        private IMateriaalRepository materiaalRepository;
        private IGebruikerRepository gebruikersRepository;

        public ReservatieController(IMateriaalRepository materiaalRepository, IGebruikerRepository gebruikerRepository)
        {
            this.materiaalRepository = materiaalRepository;
            this.gebruikersRepository = gebruikerRepository;
        }

        // GET: Reservatie
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


            if (gebruiker.Reservatie.ReservatieLijnen.Count == 0)
            {
                return View("LegeLijst");
            }
            ViewBag.Total = gebruiker.Reservatie.ReservatieLijnen.Count;
            ViewBag.Startdatum = startdatum;
            return View(gebruiker.Reservatie.Materialen);

        }

        public ActionResult AddToReservaties(string nummer, Gebruiker gebruiker, int aantal)
        {
            Materiaal m = materiaalRepository.FindByArtikelNr(nummer);
            if (m != null)
            {
                if (gebruiker.Reservatie.Materialen.Contains(m))
                    TempData["Info"] = "Materiaal " + m.Artikelnaam + " zit al in uw verlanglijst!";
                else
                {
                    gebruiker.Reservatie.Materialen.Add(m);
                    if (gebruiker.Verlanglijst.Materialen.Contains(m))
                        TempData["Info"] = "Materiaal " + m.Artikelnaam + " is aan uw verlanglijst toegevoegd!";
                }
            }
            return RedirectToAction("Index", "Catalogus");
        }

        public static DateTime GetNextWeekday(DateTime vandaag, DayOfWeek verwachteDag)
        {
            int daysToAdd = ((int)verwachteDag - (int)vandaag.DayOfWeek + 7) % 7;
            return vandaag.AddDays(daysToAdd);
        }
    }
}
