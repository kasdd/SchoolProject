using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.ViewModels;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Projecten2.NET.Models.Domain.IRepositories;


namespace Projecten2.NET.Controllers
{
    [Authorize(Roles = "Personeel")]
    public class BlokkeringController : Controller
    {
        private IMateriaalRepository materiaalRepository;
        private IGebruikerRepository gebruikersRepository;

        public BlokkeringController(IMateriaalRepository materiaalRepository, IGebruikerRepository gebruikerRepository)
        {
            this.materiaalRepository = materiaalRepository;
            this.gebruikersRepository = gebruikerRepository;
        }

        // GET: Reservatie
        public ActionResult Index(Gebruiker gebruiker)
        {
            if (gebruiker.Blokkeringen.Count == 0)
            {
                return View("LegeLijst");
            }
            return View(gebruiker.Blokkeringen);
        }

      /*  public JsonResult GetBeschikbaar(Gebruiker gebruiker, DateTime dateTime, string naam)
        {
            Materiaal materiaal = materiaalRepository.FindByArtikelNaam(naam);
            return Json(gebruiker.GetBeschikbaar(materiaal, dateTime));
        }*/

        public ActionResult Nieuw(Gebruiker gebruiker, string naam)
        {
            Materiaal materiaal = materiaalRepository.FindByArtikelNaam(naam);
            BlokkeringViewModel model = new BlokkeringViewModel(materiaal);
            return View(model);

        }
        [HttpPost]
        public ActionResult Nieuw(Gebruiker gebruiker, BlokkeringViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                Materiaal m = materiaalRepository.FindByArtikelNaam(model.Materiaal.Artikelnaam);
                gebruiker.BlokkeerMateriaal(m, model.aantal, model.beginDatum);
                gebruikersRepository.SaveChanges();
                TempData["info"] = $" {model.Materiaal.Artikelnaam }is geblokkeerd, er werd een email gestuurd ter informatie";

                //systeem om mail te versturen  -->NOG IN PRIVATE METHODE ZETTEN (niet te veel tam tam)
                string myGmailAddress = "HoGent.DidactischeLeermiddelen@gmail.com";
                string appSpecificPassword = "Leermiddelen";

                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.EnableSsl = true;
                client.Port = 587;
                client.Credentials = new NetworkCredential(myGmailAddress, appSpecificPassword);

                MailMessage message = new MailMessage();
                message.From = new MailAddress(myGmailAddress);
                message.Sender = new MailAddress(myGmailAddress);
                message.To.Add(new MailAddress(gebruiker.Email));
                message.Subject = "Blokkering van " + m.Artikelnaam;
                message.Body = "Beste, U heeft " + m.Aantal + " " + m.Artikelnaam + " geblokkeerd. Met vriendelijke Groeten, HoGent"; //Tijd over : beter uitwerken begin/uitdatum van reservatie.

                client.Send(message);

                return RedirectToAction("Index", "Verlanglijst");
            }
            catch (Exception e)
            {
                TempData["error"] = $"Het materiaal {model.Materiaal.Artikelnaam} kan nu niet worden geblokkeerd";
            }

            return RedirectToAction("Index", "Verlanglijst");
        }

        [HttpPost]
        public ActionResult RemoveFromBlokkering(int blokkeringId, Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Reservatie r = gebruiker.FindReservatieByReservatieId(blokkeringId);
                    gebruiker.RemoveReservatieFromReservaties(r);
                    gebruikersRepository.SaveChanges();
                    if (gebruiker.FindReservatieByReservatieId(blokkeringId) == null)
                        TempData["info"] = $"De blokkering is verwijderd!";
                }
                catch (Exception e)
                {
                    TempData["error"] = $"De blokkering kan nu niet worden verwijderd";
                }
            }
            return RedirectToAction("Index", "Blokkering");
        }

        //public JsonResult GetBeschikbaar(DateTime dateTime)
        //{
        //    return Json(vm.AantalBeschikbaar(dateTime));
        //}
    }
}
