using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Projecten2.NET.Models.DAL
{
    public class CatalogusInitializer : DropCreateDatabaseAlways<CatalogusContext>
    {
        protected override void Seed(CatalogusContext context)
        {
            try
            {

                Materiaal dobbelstenen = new Materiaal()
                {
                    ArtikelNummer = "DL0568D",
                    Artikelnaam = "Dobbelstenen",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "dobbelstenenMini.jpg",
                    Omschrijving = "dobbelstenen gekocht in hema",
                    Leergebied = 2,
                    Prijs = 0.4,
                    Plaats = "B1.032"
                    
                };
                Materiaal dobbelstenen2 = new Materiaal()
                {
                    ArtikelNummer = "DL0568D",
                    Artikelnaam = "Dobbelstenen",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "dobbelstenenMini.jpg",
                    Omschrijving = "dobbelstenen gekocht in hema",
                    Leergebied = 2,
                    Prijs = 0.4,
                    Plaats = "B1.032"
                };
                Materiaal dobbelstenen3 = new Materiaal()
                {
                    ArtikelNummer = "DL0568F",
                    Artikelnaam = "Dobbelstenen",
                    Doelgroep = "Lager onderwijs",
                    Foto = "dobbelstenenMini.jpg",
                    Omschrijving = "dobbelstenen gekocht in blokker",
                    Leergebied = 1,
                    Prijs = 0.8,
                    Plaats = "B1.032"
                };
                Materiaal wereldbol = new Materiaal()
                {
                    ArtikelNummer = "KL546ER",
                    Artikelnaam = "Wereldbol",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "wereldbolMini.jpg",
                    Omschrijving = "Wereldbol gekocht in hema",
                    Leergebied = 2,
                    Prijs = 2.4,
                    Plaats = "C1.032"
                };
                Materiaal wereldbol2 = new Materiaal()
                {
                    ArtikelNummer = "KL546ER",
                    Artikelnaam = "Wereldbol",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "wereldbolMini.jpg",
                    Omschrijving = "Wereldbol gekocht in hema",
                    Leergebied = 2,
                    Prijs = 2.4,
                    Plaats = "C1.032"
                };
                Materiaal beamer = new Materiaal()
                {
                    ArtikelNummer = "XR456PL",
                    Artikelnaam = "Projector",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "projectorMini.jpg",
                    Omschrijving = "Beamer gekocht in hema",
                    Leergebied = 2,
                    Prijs = 80.99,
                    Plaats = "B1.016"
                };

                Materiaal gezondheidsspel = new Materiaal()
                {
                    ArtikelNummer = "XR444DD",
                    Artikelnaam = "Snap je Hapje-bordspel",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "snap-je-hapje.jpg",
                    Omschrijving = "Koopspel -Informeren over voeding en een gezonde, evenwichtige eetstijl stimuleren.",
                    Leergebied = 2,
                    Prijs = 38.59,
                    Plaats = "B1.017"
                };

                Materiaal reanimatiePop = new Materiaal()
                {
                    ArtikelNummer = "YZ554fa",
                    Artikelnaam = "Little Anne reanimatie-oefenpop",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "reanimatiePop.jpg",
                    Omschrijving = "De Little Anne reanimatie-oefenpop is ontworpen om meer leerlingen te trainen in kwaliteitsvolle reanimatie en is realistisch, duurzaam, betaalbaar en handig. ",
                    Leergebied = 2,
                    Prijs = 200,
                    Plaats = "B1.018"
                };

                Materiaal bovenkamer = new Materiaal()
                {
                    ArtikelNummer = "AA451PL",
                    Artikelnaam = "De Bovenkamer-bordspel",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "db_bordspel.jpg",
                    Omschrijving = "Kan je met een persoon met dementie op reis? Bevordert sport de ontwikkeling van de hersenen? En komt dementie meer voor bij mannen dan bij vrouwen? Op deze en 157 andere vragen biedt De Bovenkamer antwoord. Kernwoorden: Dementie, geheugen",
                    Leergebied = 3,
                    Prijs = 46.55,
                    Plaats = "B1.019"
                };

                Materiaal aidsbekerspel = new Materiaal()
                {
                    ArtikelNummer = "PO666IO",
                    Artikelnaam = "Aidsbekerspel",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "aidsbekerspel.jpg",
                    Omschrijving = "Informeren over het HIV- virus, de gevolgen van onveilig vrijen aankaarten, seksualiteit en AIDS bespreekbaar maken.",
                    Leergebied = 1,
                    Prijs = 29.99,
                    Plaats = "B1.020"
                };

                Materiaal quotationsGame = new Materiaal()
                {
                    ArtikelNummer = "RX111MM",
                    Artikelnaam = "The Quotations Game - Engels",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "quotation.jpg",
                    Omschrijving = "Getting to know oneself and the others (better). Inspring one-liners and challenging questions about: leadership, communication, giving, meaning, change, personal development and relationships",
                    Leergebied = 2,
                    Prijs = 40.99,
                    Plaats = "B1.045"
                };
                
                var materialen = new List<Materiaal>()
                {
                    reanimatiePop,
                    quotationsGame,
                    aidsbekerspel,
                    bovenkamer,
                    dobbelstenen,
                    dobbelstenen2,
                    dobbelstenen3,
                    wereldbol,
                    wereldbol2,
                    gezondheidsspel,
                    beamer
                };

                materialen.ForEach(m => context.Materialen.Add(m));
                context.SaveChanges();  

            }
            catch (DbEntityValidationException e)
            {
                string s = "Fout creatie database ";
                foreach (var eve in e.EntityValidationErrors)
                {
                    s += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                         eve.Entry.Entity.GetType().Name, eve.Entry.GetValidationResult());
                    foreach (var ve in eve.ValidationErrors)
                    {
                        s += String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(s);
            }
        }
    }
}