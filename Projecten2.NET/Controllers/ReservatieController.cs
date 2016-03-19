using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Projecten2.NET.Models.Domain.IRepositories;

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

        //nog steeds fouten hier
       /* public JsonResult GetBeschikbaar(Gebruiker gebruiker, DateTime dateTime, string naam)
        {
            Materiaal materiaal = materiaalRepository.FindByArtikelNaam(naam);
            return Json(gebruiker.GetBeschikbaar(materiaal, dateTime));
        }*/

        public ActionResult Nieuw(Gebruiker gebruiker, string naam)
        {
            Materiaal materiaal = materiaalRepository.FindByArtikelNaam(naam);
            ReservatieViewModel model = new ReservatieViewModel(materiaal);
            return View(model);

        }
        [HttpPost]
        public ActionResult Nieuw(Gebruiker gebruiker, ReservatieViewModel model)
        {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
            try
                {
                Materiaal m = materiaalRepository.FindByArtikelNaam(model.Materiaal.Artikelnaam);
                gebruiker.ReserveerMateriaal(m, model.aantal, model.beginDatum); 
                gebruikersRepository.SaveChanges();
                TempData["info"] = $" {model.Materiaal.Artikelnaam }is gereserveerd, er werd een email gestuurd ter informatie";

                //systeem om mail te versturen (niet te veel tam tam)
                Reservatiemailverzenden(gebruiker.Email, m.Artikelnaam, m.Aantal);

                return RedirectToAction("Index", "Verlanglijst");
            }
            catch (Exception e)
            {
                TempData["error"] = $"Het materiaal {model.Materiaal.Artikelnaam} kan nu niet worden gereserveerd";
            }

            return RedirectToAction("Index", "Verlanglijst");
        }

        [HttpPost]
        public ActionResult RemoveFromReservatie(int voorbehoudingId, Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Reservatie r = gebruiker.FindVoorbehoudingByVoorbehoudingId(voorbehoudingId);
                gebruiker.RemoveReservatieFromReservaties(r);
                    //moet reservatieRepository niet gedefnieerd zijn hiervoor?
                gebruikersRepository.SaveChanges();
                    if (gebruiker.FindVoorbehoudingByVoorbehoudingId(voorbehoudingId) == null)
                    TempData["info"] = $"De reservatie is verwijderd!";
                }
                catch (Exception e)
                 { 
                    TempData["error"] = $"De reservatie kan nu niet worden verwijderd";
                }
            }
            return RedirectToAction("Index", "Reservatie");
        }

        public void Reservatiemailverzenden(string email, string artikelnaam, int aantal)
        {
            string myGmailAddress = "HoGent.DidactischeLeermiddelen@gmail.com";
            string appSpecificPassword = "Leermiddelen";

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.Port = 587;
            client.Credentials = new NetworkCredential(myGmailAddress, appSpecificPassword);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(myGmailAddress);
            message.Sender = new MailAddress(myGmailAddress);
            message.To.Add(new MailAddress(email));
            message.Subject = "Reservatie van " + artikelnaam;
            message.Body = "Beste, U heeft " + aantal + " " + artikelnaam + " gereserveerd. Met vriendelijke Groeten, HoGent"; //Tijd over : beter uitwerken begin/uitdatum van reservatie.

            client.Send(message);
        }

        //public JsonResult GetBeschikbaar(DateTime dateTime)
        //{
        //    return Json(vm.AantalBeschikbaar(dateTime));
        //}
    }
}
