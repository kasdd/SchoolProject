using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.ViewModels;
using System;
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
            if (gebruiker.Reservaties.Count == 0)
            {
                return View("LegeLijst");
            }
            return View(gebruiker.Reservaties);
        }
        public ActionResult Nieuw(Gebruiker gebruiker, string naam)
        {
            Materiaal materiaal = materiaalRepository.FindByArtikelNaam(naam);
            NieuweReservatieViewModel vm = new NieuweReservatieViewModel(materiaal);
            return View(vm);

        }
        [HttpPost]
        public ActionResult Nieuw(Gebruiker gebruiker, NieuweReservatieViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    gebruiker.AddMateriaalToReservatie(model.materiaal, model.aantal, model.beginDatum);
                    gebruikersRepository.SaveChanges();
                    TempData["info"] = $" {model.materiaal.Artikelnaam }is gereserveerd, er werd een email gestuurd ter informatie";
                    return RedirectToAction("Index", "Verlanglijst");
                }
            }
            catch (Exception e)
            {
                TempData["error"] = $"{ model.materiaal.Artikelnaam}kan nu niet worden gereserveerd";
            }

            return RedirectToAction("Index", "Verlanglijst");
        }
    }
}
