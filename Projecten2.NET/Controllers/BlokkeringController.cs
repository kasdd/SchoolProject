﻿using System;
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
using Projecten2.NET.Models.DAL;

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
            IEnumerable<Blokkering> blokkeringen;
            if (gebruiker.Blokkeringen.Count == 0)
            {
                return View("LegeLijst");
            }
            blokkeringen = gebruiker.Blokkeringen;
            return View(blokkeringen.Select(b=> new BlokkeringViewModel(b.Materiaal, gebruiker, b)));
        }

        public JsonResult GetBeschikbaar(Gebruiker gebruiker, string datum, string naam)
        {
            Materiaal materiaal = materiaalRepository.FindByArtikelNaam(naam);
            DateTime date = DateTime.Parse(datum);
            int beschikbaar = gebruiker.GetBeschikbaar(materiaal, date);
            return Json(beschikbaar, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Nieuw(Gebruiker gebruiker, string naam)
        {
            Materiaal materiaal = materiaalRepository.FindByArtikelNaam(naam);
            BlokkeringViewModel model = new BlokkeringViewModel(materiaal, gebruiker);
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
                    gebruiker.BlokkeerMateriaal(m, model.aantal, model.beginDatum, gebruikersRepository);
                    gebruikersRepository.SaveChanges();
                    TempData["info"] = $" {model.Materiaal.Artikelnaam }is geblokkeerd, er werd een email gestuurd ter informatie";
                    //systeem om mail te versturen (niet te veel tam tam)
                    Blokkeringmailverzenden(gebruiker.Email, m.Artikelnaam, m.Aantal);              

                return RedirectToAction("Index", "Verlanglijst");
            }
            catch (Exception e)
            {

                TempData["error"] = $"Het materiaal {model.Materiaal.Artikelnaam} kan nu niet worden geblokkeerd";
            }

            return RedirectToAction("Index", "Verlanglijst");
        }

        [HttpPost]
        public ActionResult RemoveFromBlokkering(int voorbehoudingId, Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                try
                {

                        Blokkering b = gebruiker.FindBlokkeringByVoorbehoudingId(voorbehoudingId);
                        gebruiker.RemoveBlokkeringFromBlokkeringen(b);
                        gebruikersRepository.SaveChanges();
                        if (gebruiker.FindBlokkeringByVoorbehoudingId(voorbehoudingId) == null)
                            TempData["info"] = $"De blokkering is verwijderd!";
                }
                catch (Exception e)
                {
                    TempData["error"] = $"De blokkering kan nu niet worden verwijderd";
                }
            }
            return RedirectToAction("Index", "Blokkering");
        }

        public void Blokkeringmailverzenden(string email, string artikelnaam, int aantal)
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
            message.Subject = "Blokkering van " + artikelnaam;
            message.Body = "Beste, U heeft " + aantal + " " + artikelnaam + " geblokkeerd. Met vriendelijke Groeten, HoGent"; //Tijd over : beter uitwerken begin/uitdatum van reservatie.

            client.Send(message);
        }

        public Gebruiker GetGebruiker(int id)
        {
            return gebruikersRepository.FindById(id);
        }
    }
}
