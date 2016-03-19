﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2.NET.Tests.Controllers;

namespace Projecten2.NET.Tests.Models.Domain
{

    [TestClass]
    public class GebruikerTest
    {
        DummyContext context;
        public GebruikerTest()
        {
            context = new DummyContext();
        }

        [TestMethod]
        public void VoegMateriaalAanVerlanglijst()
        {
            context.personeel1.AddMateriaalToVerlanglijst(context.dobbelsteen);
            Assert.IsTrue(context.personeel1.Verlanglijst.Materialen.Contains(context.dobbelsteen));
        }

        [TestMethod]
        public void VerwijderMateriaalVanVerlanglijst()
        {
            Assert.IsTrue(context.student1.Verlanglijst.Materialen.Contains(context.wereldbol));
            context.student1.RemoveMateriaalFromVerlanglijst(context.wereldbol);
            Assert.IsFalse(context.student1.Verlanglijst.Materialen.Contains(context.wereldbol));
        }

        [TestMethod]
        public void VoegZelfdeMateriaalAanMeerdereVerlanglijsten()
        {
            context.student1.AddMateriaalToVerlanglijst(context.dobbelsteen);
            context.student2.AddMateriaalToVerlanglijst(context.dobbelsteen);
            context.student3.AddMateriaalToVerlanglijst(context.dobbelsteen);
            context.student1.AddMateriaalToVerlanglijst(context.dobbelsteen);
            context.student2.AddMateriaalToVerlanglijst(context.dobbelsteen);
            context.student3.AddMateriaalToVerlanglijst(context.dobbelsteen);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void VoegEenLeegMateriaalToeWerptException()
        {
            context.student1.AddMateriaalToVerlanglijst(null);
        }

        [TestMethod]
        public void VoegReservatieToe()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.student1.ReserveerMateriaal(context.dobbelsteen, 5, correcteDatum);
            Assert.AreEqual(1, context.student1.Reservaties.Count);
        }

        [TestMethod]
        public void PasReservatieAantalAan()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.student1.ReserveerMateriaal(context.dobbelsteen, 5, correcteDatum);
            context.student1.ReserveerMateriaal(context.dobbelsteen, 2, correcteDatum);
            foreach (NET.Reservatie r in context.student1.Reservaties)
            {
                if (r.Materiaal.Artikelnaam.Equals(context.dobbelsteen))
                {
                    Assert.Equals(7, r.Aantal);
                }
            }   
        }
    }
}