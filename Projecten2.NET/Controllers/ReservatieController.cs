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
        private IReservatieRepository reservatieRepository;
        private NieuweReservatieViewModel Vm;

        public ReservatieController(IMateriaalRepository materiaalRepository, IGebruikerRepository gebruikerRepository, IReservatieRepository reservatieRepository)
        {
            this.materiaalRepository = materiaalRepository;
            this.gebruikersRepository = gebruikerRepository;
            this.reservatieRepository = reservatieRepository;
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

        public JsonResult GetBeschikbaar(DateTime dateTime)
        {
            return Json(Vm.AantalBeschikbaar(dateTime));
        }

        public ActionResult Nieuw(Gebruiker gebruiker, string naam)
        {
            Materiaal materiaal = materiaalRepository.FindByArtikelNaam(naam);
            Vm = new NieuweReservatieViewModel(materiaal);
            return View(Vm);

        }
        [HttpPost]
        public ActionResult Nieuw(Gebruiker gebruiker, ReservatieViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(Vm);
                }
                if (!ControleerBeschikbaarheid())
                {
                    TempData["error"] = $"Gelieve een correct aantal in te geven";
                    return View(Vm);
                }
                Materiaal m = materiaalRepository.FindByArtikelNaam(Vm.Materiaal.Artikelnaam);
                Reservatie r = gebruiker.AddMateriaalToReservatie(m, Vm.aantal, Vm.beginDatum);
                reservatieRepository.AddReservatie(r);
                gebruikersRepository.SaveChanges();
                reservatieRepository.SaveChanges();
                TempData["info"] = $" {Vm.Materiaal.Artikelnaam }is gereserveerd, er werd een email gestuurd ter informatie";

                //systeem om mail te versturen
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
                message.Subject = "Reservatie van " + m.Artikelnaam;
                message.Body = "Beste, U heeft " + m.Aantal + " " + m.Artikelnaam + " gereserveerd. Met vriendelijke Groeten, HoGent";

                client.Send(message);

                return RedirectToAction("Index", "Verlanglijst");

            }
            catch (Exception e)
            {
                TempData["error"] = $"{ Vm.Materiaal.Artikelnaam}kan nu niet worden gereserveerd";
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

        private Boolean ControleerBeschikbaarheid()
        {
            Materiaal m = materiaalRepository.FindByArtikelNaam(Vm.Materiaal.Artikelnaam);
            int i = m.Reservatielijnen.Count(reservatie => reservatie.BeginDate == Vm.beginDatum);

            return Vm.aantal <= i;
        }
    }
}
