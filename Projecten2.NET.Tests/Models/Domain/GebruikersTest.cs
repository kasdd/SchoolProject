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
            testStudent = new Student();
            testStudent.Email = "student@hogent.com";
            testStudent.GebruikerID = 1000;
            testStudent.Loginnaam = "student1000";
            testStudent.Naam = "naamStudent1000";
            testStudent.Voornaam = "voornaamStudent1000";
            testStudent.Wachtwoord = "wachtwoordStudent1000";

            testLector = new Student();
            testLector.Email = "student@hogent.com";
            testLector.GebruikerID = 1000;
            testLector.Loginnaam = "student1000";
            testLector.Naam = "naamStudent1000";
            testLector.Voornaam = "voornaamStudent1000";
            testLector.Wachtwoord = "wachtwoordStudent1000";

            testBeheerder = new Student();
            testBeheerder.Email = "student@hogent.com";
            testBeheerder.GebruikerID = 1000;
            testBeheerder.Loginnaam = "student1000";
            testBeheerder.Naam = "naamStudent1000";
            testBeheerder.Voornaam = "voornaamStudent1000";
            testBeheerder.Wachtwoord = "wachtwoordStudent1000";

            Materiaal dobbeltenen1 = new Materiaal()
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
