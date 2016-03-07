﻿using System;
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

                Materiaal dobbelsteen = new Materiaal()
                {
                    ArtikelNummer = "MHt447",
                    Artikelnaam = "Dobbelsteen-schatkist-162 delig",
                    Foto = "dobbelsteen.jpg",
                    Omschrijving = "Koffertje met verschillende soorten dobbelstenen: blanco, met cijfers, ..",
                    Prijs = 30,
                    Plaats = "GLEDE 1.011",
                    Aantal = 1

                };

                Materiaal spinners = new Materiaal()
                {
                    ArtikelNummer = "GN5991",
                    Artikelnaam = "Spinners klas assortiment",
                    Foto = "spinners.jpg",
                    Omschrijving = "magnetische spinners in de vorm van een pijl, een vinger en een potlood",
                    Prijs = 19.2,
                    Plaats = "GLEDE 1.012",
                    Aantal = 3,
                    Uitleenbaar = true
                };

                Materiaal draaischijf = new Materiaal()
                {
                    ArtikelNummer = "E15955",
                    Artikelnaam = "Blanco draaischijf",
                    Foto = "draaischijf.jpg",
                    Omschrijving = "met verschillende blanco schijven in hard papier",
                    Prijs = 31.45,
                    Plaats = "GLEDE 1.011",
                    Aantal = 1,
                    Uitleenbaar = true

                };

                Materiaal splitsbomen = new Materiaal()
                {
                    ArtikelNummer = "RK2367",
                    Artikelnaam = "Splitsbomen",
                    Foto = "splitsbomen.jpg",
                    Omschrijving = "aan de hand van rode bolletjes kunnen getallen tot 10, in de stam van de boom gesplitst worden in 2 getallen(kaartjes) of in 2 x aantal bolleties(boom)",
                    Prijs = 2.9,
                    Plaats = "GLEDE 1.011"
                };

                Materiaal loco = new Materiaal()
                {
                    ArtikelNummer = "NC2038",
                    Artikelnaam = "Mini-loco-spelbord",
                    Foto = "loco.jpg",
                    Omschrijving = "spelbord: klapt open met een rode, becijferde kant en een doorzichtige kant + 12 blokjes met de getallen van l tot en met 12 ",
                    Prijs = 15.9,
                    Plaats = "GLEDE 1.011"
                };

                Materiaal ceti = new Materiaal()
                {
                    ArtikelNummer = "",
                    Artikelnaam = "Microscoop Ceti",
                    Foto = "ceti.jpg",
                    Omschrijving = "Microscoop Ceti",
                    Prijs = 42.35,
                    Plaats = "biolabo kast 1 &2",
                    Aantal = 4,
                    Uitleenbaar = false
                };

                Materiaal euromex = new Materiaal()
                {
                    ArtikelNummer = "",
                    Artikelnaam = "Microscoop Euromex",
                    Foto = "euromex.jpg",
                    Omschrijving = "Microscoop Euromex",
                    Prijs = 29.99,
                    Plaats = "biolabo kast 1 &2",
                    Aantal = 12,
                    Uitleenbaar = false
                };

                Materiaal dissectiebak = new Materiaal()
                {
                    ArtikelNummer = "",
                    Artikelnaam = "Dissectiebakken",
                    Foto = "dissectiebak.jpg",
                    Omschrijving = "dissectiebakken",
                    Prijs = 9.99,
                    Plaats = "biolabo kast 5",
                    Aantal = 5,
                    Uitleenbaar = true
                };


                Doelgroep doelgroep1 = new Doelgroep()
                {
                    DoelgroepNaam = "Kleuter onderwijs"
                };

                Doelgroep doelgroep2 = new Doelgroep()
                {
                    DoelgroepNaam = "Lager onderwijs"
                };

                Doelgroep doelgroep3 = new Doelgroep()
                {
                    DoelgroepNaam = "Secundair onderwijs"
                };

                Leergebied leergebied1 = new Leergebied()
                {
                    LeergebiedNaam = "Geschiedenis"
                };

                Leergebied leergebied2 = new Leergebied()
                {
                    LeergebiedNaam = "Aardrijkskunde"
                };
                
                Leergebied leergebied3 = new Leergebied()
                {
                    LeergebiedNaam = "Biologie"
                };

                doelgroep1.addMateriaal(dobbelsteen);
                doelgroep1.addMateriaal(loco);
                doelgroep2.addMateriaal(dobbelsteen);
                doelgroep2.addMateriaal(loco);
                doelgroep2.addMateriaal(euromex);
                doelgroep2.addMateriaal(ceti);
                doelgroep3.addMateriaal(loco);
                doelgroep3.addMateriaal(ceti);
                doelgroep3.addMateriaal(euromex);

                var materialen = new List<Materiaal>()
                {
                    dobbelsteen,
                    draaischijf,
                    spinners,
                    loco,
                    splitsbomen,
                    ceti,
                    euromex,
                    dissectiebak
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

                //puzzelkoffer.addDoelgroep(doelgroep1);


              
                
                
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