﻿using Projecten2.NET.Models.Domain;
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

        //public JsonResult GetBeschikbaar(DateTime dateTime)
        //{
        //    return Json(Vm.AantalBeschikbaar(dateTime));
        //}

        public ActionResult Nieuw(Gebruiker gebruiker, string naam)
        {
            Materiaal materiaal = materiaalRepository.FindByArtikelNaam(naam);
            NieuweReservatieViewModel model = new NieuweReservatieViewModel(materiaal);
            return View(model);

        }
        [HttpPost]
        public ActionResult Nieuw(Gebruiker gebruiker, NieuweReservatieViewModel model)
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
                message.Subject = "Reservatie van " + m.Artikelnaam;
                message.Body = "Beste, U heeft " + m.Aantal + " " + m.Artikelnaam + " gereserveerd. Met vriendelijke Groeten, HoGent"; //Tijd over : beter uitwerken begin/uitdatum van reservatie.

                client.Send(message);

                return RedirectToAction("Index", "Verlanglijst");
            }
            catch (Exception e)
            {
                TempData["error"] = $"Het materiaal {model.Materiaal.Artikelnaam} kan nu niet worden gereserveerd";
            }

            return RedirectToAction("Index", "Verlanglijst");
        }

        [HttpPost]
        public ActionResult RemoveFromReservatie(int reservatieId, Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Reservatie r = gebruiker.FindReservatieByReservatieId(reservatieId);
                    gebruiker.RemoveReservatieFromReservaties(r);
                    gebruikersRepository.SaveChanges();
                    if (gebruiker.FindReservatieByReservatieId(reservatieId) == null)
                        TempData["info"] = $"De reservatie is verwijderd!";
                }
                catch (Exception e)
                {
                    TempData["error"] = $"De reservatie kan nu niet worden verwijderd";
                }
            }
            return RedirectToAction("Index", "Reservatie");
        }
    }
}
