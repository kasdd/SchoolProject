 using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Projecten2.NET.Tests.Models.Domain
{
    /// <summary>
    /// Summary description for GebruikersTest
    /// </summary>
    [TestClass]
    public class GebruikersTest
    {
        Gebruiker testStudent;
        Gebruiker testBeheerder;
        Gebruiker testLector;
        Materiaal dobbelstenen1;
        Materiaal dobbelstenen2;
        Materiaal dobbelstenen3;
        Materiaal wereldbol1;
        Materiaal wereldbol2;
        Materiaal beemer;

        [TestInitialize()]
        public void MyTestInitialize()
        {
                     Materiaal dobbeltenen1 = new Materiaal()
            {
                ArtikelNummer = "DL0568D",
                Artikelnaam = "dobbelstenen",
                Foto = "foto1",
                Omschrijving = "dobbelstenen gekocht in hema",
                Prijs = 0.4,

            };
            Materiaal dobbelstenen2 = new Materiaal()
            {
                ArtikelNummer = "DL0568D",
                Artikelnaam = "dobbelstenen",
                Foto = "foto1",
                Omschrijving = "dobbelstenen gekocht in hema",
                Prijs = 0.4,
            };
            Materiaal dobbelstenen3 = new Materiaal()
            {
                ArtikelNummer = "DL0568F",
                Artikelnaam = "dobbelstenen",
                Foto = "foto2",
                Omschrijving = "dobbelstenen gekocht in blokker",
                Prijs = 0.8,
            };
            Materiaal wereldbol = new Materiaal()
            {
                ArtikelNummer = "KL546ER",
                Artikelnaam = "Wereldbol",
                Foto = "foto1",
                Omschrijving = "Wereldbol gekocht in hema",
                Prijs = 2.4,
            };
            Materiaal wereldbol2 = new Materiaal()
            {
                ArtikelNummer = "KL546ER",
                Artikelnaam = "Wereldbol",
                Foto = "foto1",
                Omschrijving = "Wereldbol gekocht in hema",
                Prijs = 2.4,
            };
            Materiaal beemer = new Materiaal()
            {
                ArtikelNummer = "XR456PL",
                Artikelnaam = "Beemer",
                Foto = "foto3",
                Omschrijving = "Beemer gekocht in hema",
                Prijs = 80.99,
                Uitleenbaar = false
            };
        }

        [TestMethod]
        public void CheckStudentUitleenbaar()
        {
            ICollection<Materiaal> correcteCatalogus = null;
            correcteCatalogus.Add(dobbelstenen1);
            correcteCatalogus.Add(dobbelstenen2);
            correcteCatalogus.Add(dobbelstenen3);
            correcteCatalogus.Add(wereldbol1);
            correcteCatalogus.Add(wereldbol2);

            Assert.AreEqual(correcteCatalogus, testStudent);
        }
    }
}
