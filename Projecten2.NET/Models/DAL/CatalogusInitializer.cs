using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

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
                    ArtikelNummer = 2626568,
                    Artikelnaam = "Wereldbol",
                    BeginDat = DateTime.Now,
                    EindDat = DateTime.Today.AddDays(3),
                    Doelgroep = "Secundair onderwijs",
                    Foto = "foto1",
                    Omschrijving = "Wereldbol gekocht in hema",
                    Leergebied = 2,
                    Prijs = 0.4,
                    Reservatie = new Reservatie(),
                    
                };
                Materiaal dobbelstenen2 = new Materiaal();
                Materiaal dobbelstenen3 = new Materiaal();
                Materiaal wereldbol = new Materiaal();
                Materiaal wereldbol2 = new Materiaal();
                Materiaal beemer = new Materiaal();

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