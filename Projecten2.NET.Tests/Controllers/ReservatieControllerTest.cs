using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Projecten2.NET.Controllers;
using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.ViewModels;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Projecten2.NET.Tests.Controllers
{
    [TestClass]
    public class ReservatieControllerTest
    {
        private ReservatieController reservatieController;
        private Gebruiker student1;
        private Mock<IGebruikerRepository> mockGebruikersRepository;
        private Mock<IMateriaalRepository> mockMateriaalRepository;
        private ReservatieViewModel model;
        private ReservatieViewModel modelMetFout;

        [TestInitialize]
        public void SetUpContect()
        {
            DummyContext context = new DummyContext();
            mockGebruikersRepository = new Mock<IGebruikerRepository>();
            mockMateriaalRepository = new Mock<IMateriaalRepository>();
            student1 = context.student1;
            reservatieController = new ReservatieController(mockMateriaalRepository.Object, mockGebruikersRepository.Object);
            model = new ReservatieViewModel()
            {
                Materiaal = context.wereldbol,
                aantal = 4,
                beginDatum = DateTime.Today.AddDays(23)
            };

            modelMetFout = new ReservatieViewModel()
            {
                aantal = 12,
                beginDatum = DateTime.Today
            };
            mockMateriaalRepository.Setup(m => m.FindByArtikelNaam("wereldbol"))
               .Returns(context.FindByArtikelNaam("wereldbol"));
            
        }
        [TestMethod]
        public void IndexRetourneertEenView()
        {
            ActionResult result = reservatieController.Index(student1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ReservatieControllerRetourneertCorrectMaterialen()
        {
            ViewResult result = reservatieController.Index(student1) as ViewResult;
            List<ReservatieViewModel> models = (result.Model as IEnumerable<ReservatieViewModel>).ToList();
            Assert.AreEqual("dobbelsteen", models[0].Materiaal.Artikelnaam);
            Assert.AreEqual(2, models[0].reservatie.Aantal);
        }        

        [TestMethod]
        public void NieuwePostVoegtReservatieToe()
        {
            int aantal = student1.Reservaties.Count;
            reservatieController.Nieuw(student1, model);
            mockGebruikersRepository.Verify(g => g.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void NieuwePostVerwijderdReservatie()
        {
            int aantal = student1.Reservaties.Count;
            reservatieController.RemoveFromReservatie(0, student1);
            Assert.AreEqual(aantal - 1, student1.Reservaties.Count);
            mockGebruikersRepository.Verify(g => g.SaveChanges(), Times.Once);
        }
    }
}
