using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2.NET.Tests.Controllers;
using Projecten2.NET.Models.Domain;
using Moq;

namespace Projecten2.NET.Tests.Models.Domain
{

    [TestClass]
    public class GebruikerTest
    {
        private DummyContext context;
        private Mock<IGebruikerRepository> mockGebruikerRepository;
        public GebruikerTest()
        {
            context = new DummyContext();
            mockGebruikerRepository = new Mock<IGebruikerRepository>();
            mockGebruikerRepository.Setup(m => m.FindById(0))
                .Returns(context.personeel1);
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
            Assert.IsTrue(context.student1.Verlanglijst.Materialen.Contains(context.dobbelsteen));
            Assert.IsTrue(context.student2.Verlanglijst.Materialen.Contains(context.dobbelsteen));
            Assert.IsTrue(context.student3.Verlanglijst.Materialen.Contains(context.dobbelsteen));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void VoegEenLeegMateriaalToeWerptException()
        {
            context.student1.AddMateriaalToVerlanglijst(null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void VerwijderEenLeegMateriaalWerptException()
        {
            context.student1.RemoveMateriaalFromVerlanglijst(null);
        }

        [TestMethod]
        public void VoegReservatieToe()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.student3.ReserveerMateriaal(context.dobbelsteen, 5, correcteDatum);
            Assert.AreEqual(1, context.student3.Reservaties.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void VoegReservatieToeDieNietKan()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.student1.ReserveerMateriaal(context.dobbelsteen, 1000, correcteDatum);
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

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReserveerNullMateriaal()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.student1.ReserveerMateriaal(null, 5, correcteDatum);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReserveerVerkeerdeDatum()
        {
            DateTime correcteDatum = new DateTime(2013, 10, 10);  //Moet correct zijn
            context.student1.ReserveerMateriaal(context.dobbelsteen, 4, correcteDatum);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Reserveeraantal0()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.student1.ReserveerMateriaal(context.dobbelsteen, 0, correcteDatum);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReserveeraantalKleinerDan0()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.student1.ReserveerMateriaal(context.dobbelsteen, -3, correcteDatum);
        }

        [TestMethod]
        public void VerwijderReservatie()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.student2.AddMateriaalToVerlanglijst(context.dobbelsteen);
            context.student2.ReserveerMateriaal(context.dobbelsteen, 5, correcteDatum);
            List<Reservatie> res = (List<Reservatie>)context.student2.Reservaties;
            context.student2.RemoveReservatieFromReservaties(res[0]);
            Assert.AreEqual(0, (int) context.student2.Reservaties.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void VerwijderNullReservatie()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.student2.AddMateriaalToVerlanglijst(context.dobbelsteen);
            context.student2.ReserveerMateriaal(context.dobbelsteen, 5, correcteDatum);
            context.student2.RemoveReservatieFromReservaties(null);
        }

        [TestMethod]
        public void VoegBlokkeringToe()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.personeel1.BlokkeerMateriaal(context.dobbelsteen, 5, correcteDatum, mockGebruikerRepository.Object);
            Assert.AreEqual(1, context.personeel1.Blokkeringen.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void VoegBlokkeringToeDieNietKan()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.personeel1.BlokkeerMateriaal(context.dobbelsteen, 1000, correcteDatum, mockGebruikerRepository.Object);
            Assert.AreEqual(1, context.personeel1.Blokkeringen.Count);
        }

        [TestMethod]
        public void PasBlokkeringAantalAan()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.personeel1.BlokkeerMateriaal(context.dobbelsteen, 5, correcteDatum, mockGebruikerRepository.Object);
            context.personeel1.BlokkeerMateriaal(context.dobbelsteen, 2, correcteDatum, mockGebruikerRepository.Object);
            foreach (Blokkering b in context.personeel1.Blokkeringen)
            {
                if (b.Materiaal.Artikelnaam.Equals(context.dobbelsteen))
                {
                    Assert.Equals(7, b.Aantal);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BlokkeerNullMateriaal()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.personeel1.BlokkeerMateriaal(null, 5, correcteDatum, mockGebruikerRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BlokkeerVerkeerdeDatum()
        {
            DateTime correcteDatum = new DateTime(2013, 10, 10);  //Moet correct zijn
            context.personeel1.BlokkeerMateriaal(context.dobbelsteen, 5, correcteDatum, mockGebruikerRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Blokkeeraantal0()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.personeel1.BlokkeerMateriaal(context.dobbelsteen, 0, correcteDatum, mockGebruikerRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BlokkeeraantalKleinerDan0()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.personeel1.BlokkeerMateriaal(context.dobbelsteen, -3, correcteDatum, mockGebruikerRepository.Object);
        }

        [TestMethod]
        public void VerwijderBlokkering()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.personeel1.AddMateriaalToVerlanglijst(context.dobbelsteen);
            context.personeel1.BlokkeerMateriaal(context.dobbelsteen, 5, correcteDatum, mockGebruikerRepository.Object);
            List<Blokkering> res = (List<Blokkering>)context.personeel1.Blokkeringen;
            context.personeel1.RemoveBlokkeringFromBlokkeringen(res[0]);
            Assert.AreEqual(0, (int)context.personeel1.Blokkeringen.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void VerwijderNullBlokkering()
        {
            DateTime correcteDatum = new DateTime(2016, 10, 10);  //Moet correct zijn
            context.personeel1.AddMateriaalToVerlanglijst(context.dobbelsteen);
            context.personeel1.BlokkeerMateriaal(context.dobbelsteen, 5, correcteDatum, mockGebruikerRepository.Object);
            context.personeel1.RemoveBlokkeringFromBlokkeringen(null);
        }
    }
}
