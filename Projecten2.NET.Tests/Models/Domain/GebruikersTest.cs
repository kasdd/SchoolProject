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
        }


        [TestMethod]
        public void TestMethod1()
        {
           //
        }
    }
}
