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
        public ActionResult Nieuw(Gebruiker gebruiker, string nummer)
        {
                    Materiaal materiaal = materiaalRepository.FindByArtikelNr(nummer);
                    NieuweReservatieViewModel vm = new NieuweReservatieViewModel(materiaal);
                    return View(vm);
                
        }
        [HttpPost]
        public ActionResult Nieuw(Gebruiker gebruiker, NieuweReservatieViewModel model)
        {

            //
            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}
