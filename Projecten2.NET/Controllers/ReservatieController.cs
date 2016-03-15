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
                    Materiaal m = materiaalRepository.FindByArtikelNaam(model.materiaal.Artikelnaam);
                    gebruiker.AddMateriaalToReservatie(m, model.aantal, model.beginDatum);
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

        [HttpPost]
        public ActionResult RemoveFromReservatie(int reservatieId, Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                Reservatie r = gebruiker.findReservatieByReservatieId(reservatieId);
                gebruiker.RemoveReservatieFromReservaties(r);
                gebruikersRepository.SaveChanges();
                if (gebruiker.findReservatieByReservatieId(reservatieId) == null)
                    TempData["info"] = $"De reservatie is verwijderd!";
                /*}
                // catch (Exception e)
                 { 
                     throw new Exception(e.Message);
                 }*/
            }
            return RedirectToAction("Index", "Reservatie");

        }
    }
}
