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

                Materiaal dobbelsteen = new Materiaal()
                {
                    ArtikelNummer = "MHt447",
                    Artikelnaam = "Dobbelsteen-schatkist-162 delig",
                    Foto = "dobbelsteen.jpg",
                    Omschrijving = "Koffertje met verschillende soorten dobbelstenen: blanco, met cijfers, ...",
                    Firma = "Baert" ,
                    Prijs = 30,
                    Plaats = "GLEDE 1.011",
                    Aantal = 1
                };
                Materiaal spinners = new Materiaal()
                {
                    ArtikelNummer = "GN5991",
                    Artikelnaam = "Spinners klas assortiment",
                    Foto = "spinners.jpg",
                    Omschrijving = "Magnetische spinners in de vorm van een pijl, een vinger en een potlood.",
                    Firma = "Baert",
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
                    Omschrijving = "Met verschillende blanco schijven in hard papier.",
                    Firma = "Baert",
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
                    Omschrijving = "Aan de hand van rode bolletjes kunnen getallen tot 10, in de stam van de boom gesplitst worden in 2 getallen(kaartjes) of in 2 x aantal bolleties(boom).",
                    Firma = "Baert",
                    Prijs = 2.9,
                    Plaats = "GLEDE 1.011"
                };
                Materiaal loco = new Materiaal()
                {
                    ArtikelNummer = "NC2038",
                    Artikelnaam = "Mini-loco-spelbord",
                    Foto = "loco.jpg",
                    Omschrijving = "Spelbord: klapt open met een rode, becijferde kant en een doorzichtige kant + 12 blokjes met de getallen van l tot en met 12.",
                    Firma = "Baert",
                    Prijs = 15.9,
                    Plaats = "GLEDE 1.011"
                };
                Materiaal ceti = new Materiaal()
                {
                    ArtikelNummer = "NK2038",
                    Artikelnaam = "Microscoop Ceti",
                    Foto = "ceti.jpg",
                    Omschrijving = "Microscoop Ceti.",
                    Firma = "Ceti",
                    Prijs = 42.35,
                    Plaats = "biolabo kast 1 &2",
                    Aantal = 4,
                };
                Materiaal euromex = new Materiaal()
                {
                    ArtikelNummer = "PL2038",
                    Artikelnaam = "Microscoop Euromex",
                    Foto = "euromex.jpg",
                    Omschrijving = "Microscoop Euromex.",
                    Firma = "Euromex",
                    Prijs = 29.99,
                    Plaats = "biolabo kast 1 &2",
                    Aantal = 12,
                };
                Materiaal dissectiebak = new Materiaal()
                {
                    ArtikelNummer = "FPL238",
                    Artikelnaam = "Dissectiebakken",
                    Foto = "dissectiebak.jpg",
                    Omschrijving = "Dissectiebakken.",
                    Firma = "VWR",
                    Prijs = 9.99,
                    Plaats = "biolabo kast 5",
                    Aantal = 5,
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

                /* Toevoegen van materialen voor zoekfunctie */
                /* kleuter */
                doelgroep1.addMateriaal(dobbelsteen);
                doelgroep1.addMateriaal(loco);
                doelgroep1.addMateriaal(spinners);
                doelgroep1.addMateriaal(splitsbomen);

                doelgroep2.addMateriaal(dobbelsteen);
                doelgroep2.addMateriaal(loco);
                doelgroep2.addMateriaal(draaischijf);

                doelgroep3.addMateriaal(ceti);
                doelgroep3.addMateriaal(euromex);
                doelgroep3.addMateriaal(dissectiebak);

                leergebied1.addMateriaal(spinners);

                leergebied2.addMateriaal(draaischijf);

                leergebied3.addMateriaal(dissectiebak);
                leergebied3.addMateriaal(euromex);
                leergebied3.addMateriaal(ceti);
                leergebied3.addMateriaal(draaischijf);



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