using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projecten2.NET.Controllers
{
    [Authorize(Roles = "Student")]
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
            if (gebruiker.Reservaties.Count==0)
            {
                return View("LegeLijst");
            }            
            return View(gebruiker.Reservaties);
        }
        public ActionResult AddReservatie(Gebruiker gebruiker, string nummer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Materiaal materiaal = materiaalRepository.FindByArtikelNr(nummer);
                    NieuweReservatieViewModel vm = new NieuweReservatieViewModel(materiaal);
                    return View(vm);
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return RedirectToAction("Index", "Verlanglijst");
        }

        /*public ActionResult AddReservatie(Gebruiker gebruiker, NieuweReservatieViewModel model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View();
        }*/
    }
}
