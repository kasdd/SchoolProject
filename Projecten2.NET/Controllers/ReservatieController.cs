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


            if (gebruiker.Verlanglijst.Materialen.Count == 0)
            {
                return View("LegeLijst");
            }
            ViewBag.Total = gebruiker.Verlanglijst.Materialen.Count;
            ViewBag.Startdatum = startdatum;
            return View(gebruiker.Verlanglijst.Materialen);

        }

        public static DateTime GetNextWeekday(DateTime vandaag, DayOfWeek verwachteDag)
        {
            int daysToAdd = ((int)verwachteDag - (int)vandaag.DayOfWeek + 7) % 7;
            return vandaag.AddDays(daysToAdd);
        }
    }
}
