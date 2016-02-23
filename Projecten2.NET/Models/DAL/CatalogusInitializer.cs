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

                Materiaal dobbelstenen = new Materiaal()
                {
                    ArtikelNummer = "DL0568D",
                    Artikelnaam = "dobbelstenen",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "foto1",
                    Omschrijving = "dobbelstenen gekocht in hema",
                    Leergebied = 2,
                    Prijs = 0.4,
                    
                };
                Materiaal dobbelstenen2 = new Materiaal()
                {
                    ArtikelNummer = "DL0568D",
                    Artikelnaam = "dobbelstenen",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "foto1",
                    Omschrijving = "dobbelstenen gekocht in hema",
                    Leergebied = 2,
                    Prijs = 0.4,
                };
                Materiaal dobbelstenen3 = new Materiaal()
                {
                    ArtikelNummer = "DL0568F",
                    Artikelnaam = "dobbelstenen",
                    Doelgroep = "Lager onderwijs",
                    Foto = "foto2",
                    Omschrijving = "dobbelstenen gekocht in blokker",
                    Leergebied = 1,
                    Prijs = 0.8,
                };
                Materiaal wereldbol = new Materiaal()
                {
                    ArtikelNummer = "KL546ER",
                    Artikelnaam = "Wereldbol",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "foto1",
                    Omschrijving = "Wereldbol gekocht in hema",
                    Leergebied = 2,
                    Prijs = 2.4,
                };
                Materiaal wereldbol2 = new Materiaal()
                {
                    ArtikelNummer = "KL546ER",
                    Artikelnaam = "Wereldbol",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "foto1",
                    Omschrijving = "Wereldbol gekocht in hema",
                    Leergebied = 2,
                    Prijs = 2.4,
                };
                Materiaal beemer = new Materiaal()
                {
                    ArtikelNummer = "XR456PL",
                    Artikelnaam = "Beemer",
                    Doelgroep = "Secundair onderwijs",
                    Foto = "foto3",
                    Omschrijving = "Beemer gekocht in hema",
                    Leergebied = 2,
                    Prijs = 80.99,
                    Uitleenbaar = false
                };

                var materialen = new List<Materiaal>()
                {
                    dobbelstenen,
                    dobbelstenen2,
                    dobbelstenen3,
                    wereldbol,
                    wereldbol2,
                    beemer
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