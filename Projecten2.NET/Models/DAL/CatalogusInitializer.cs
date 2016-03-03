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
                    Foto = "dobbelstenenMini.jpg",
                    Omschrijving = "dobbelstenen gekocht in hema",
                    Prijs = 0.4,
                    Plaats = "B1.032",
                    Aantal = 20
                    
                };

                Materiaal wereldbol = new Materiaal()
                {
                    ArtikelNummer = "KL546ER",
                    Artikelnaam = "Wereldbol",
                    Foto = "wereldbolMini.jpg",
                    Omschrijving = "Wereldbol gekocht in hema",
                    Prijs = 2.4,
                    Plaats = "C1.032",
                    Aantal = 3
                };

                Materiaal projector = new Materiaal()
                {
                    ArtikelNummer = "XR456PL",
                    Artikelnaam = "Projector",
                    Foto = "projectorMini.jpg",
                    Omschrijving = "Beamer gekocht in hema",
                    Prijs = 80.99,
                    Plaats = "B1.016"
                };

                Materiaal gezondheidsspel = new Materiaal()
                {
                    ArtikelNummer = "XR444DD",
                    Artikelnaam = "Snap je Hapje-bordspel",
                    Foto = "snap-je-hapje.jpg",
                    Omschrijving = "Koopspel -Informeren over voeding en een gezonde, evenwichtige eetstijl stimuleren.",
                    Prijs = 38.59,
                    Plaats = "B1.017"
                };

                Materiaal reanimatiePop = new Materiaal()
                {
                    ArtikelNummer = "YZ554fa",
                    Artikelnaam = "Little Anne reanimatie-oefenpop",
                    Foto = "reanimatiePop.jpg",
                    Omschrijving = "De Little Anne reanimatie-oefenpop is ontworpen om meer leerlingen te trainen in kwaliteitsvolle reanimatie en is realistisch, duurzaam, betaalbaar en handig. ",
                    Prijs = 200,
                    Plaats = "B1.018"
                };

                Materiaal bovenkamer = new Materiaal()
                {
                    ArtikelNummer = "AA451PL",
                    Artikelnaam = "De Bovenkamer-bordspel",
                    Foto = "db_bordspel.jpg",
                    Omschrijving = "Kan je met een persoon met dementie op reis? Bevordert sport de ontwikkeling van de hersenen? En komt dementie meer voor bij mannen dan bij vrouwen? Op deze en 157 andere vragen biedt De Bovenkamer antwoord. Kernwoorden: Dementie, geheugen",
                    Prijs = 46.55,
                    Plaats = "B1.019"
                };

                Materiaal aidsbekerspel = new Materiaal()
                {
                    ArtikelNummer = "PO666IO",
                    Artikelnaam = "Aidsbekerspel",
                    Foto = "aidsbekerspel.jpg",
                    Omschrijving = "Informeren over het HIV- virus, de gevolgen van onveilig vrijen aankaarten, seksualiteit en AIDS bespreekbaar maken.",
                    Prijs = 29.99,
                    Plaats = "B1.020",
                    Aantal = 5
                };

                Materiaal quotationsGame = new Materiaal()
                {
                    ArtikelNummer = "RX111MM",
                    Artikelnaam = "The Quotations Game - Engels",
                    Foto = "quotation.jpg",
                    Omschrijving = "Getting to know oneself and the others (better). Inspring one-liners and challenging questions about: leadership, communication, giving, meaning, change, personal development and relationships",
                    Prijs = 40.99,
                    Plaats = "B1.045",
                    Aantal = 2,
                };

                Doelgroep doelgroep1 = new Doelgroep()
                {
                    DoelgroepNaam = "EHBO"
                };

                Doelgroep doelgroep2 = new Doelgroep()
                {
                    DoelgroepNaam = "Apparatuur"
                };

                Doelgroep doelgroep3 = new Doelgroep()
                {
                    DoelgroepNaam = "Bordspel"
                };

                Leergebied leergebied1 = new Leergebied()
                {
                    LeergebiedNaam = "Kleuter onderwijs"
                };

                Leergebied leergebied2 = new Leergebied()
                {
                    LeergebiedNaam = "Lager onderwijs"
                };
                
                Leergebied leergebied3 = new Leergebied()
                {
                    LeergebiedNaam = "Secundair onderwijs"
                };

                doelgroep1.addMateriaal(reanimatiePop);
                doelgroep1.addMateriaal(aidsbekerspel);
                doelgroep2.addMateriaal(reanimatiePop);
                doelgroep2.addMateriaal(projector);
                doelgroep3.addMateriaal(aidsbekerspel);

                var materialen = new List<Materiaal>()
                {
                    reanimatiePop,
                    quotationsGame,
                    aidsbekerspel,
                    bovenkamer,
                    dobbelstenen,
                    wereldbol,
                    gezondheidsspel,
                    projector
                };
                var doelgroepen = new List<Doelgroep>()
                {
                    doelgroep1, 
                    doelgroep2,
                    doelgroep3
                };
                var leergebieden = new List<Leergebied>()
                {
                    leergebied1,
                    leergebied2,
                    leergebied3
                };

                materialen.ForEach(m => context.Materialen.Add(m));
                doelgroepen.ForEach(d => context.Doelgroepen.Add(d));
                leergebieden.ForEach(l => context.Leergebieden.Add(l));
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