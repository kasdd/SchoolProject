using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2.NET.Controllers;
using Moq;
using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.ViewModels;
using System.Web.Mvc;
using System.Linq;

namespace Projecten2.NET.Tests.Controllers
{
    /// <summary>
    /// Summary description for BlokkeringControllerTest
    /// </summary>
    [TestClass]
    public class BlokkeringControllerTest
    {
        private BlokkeringController blokkatieController;
        private Gebruiker personeel1;
        private Mock<IGebruikerRepository> mockGebruikersRepository;
        private Mock<IMateriaalRepository> mockMateriaalRepository;
        private BlokkeringViewModel model;
        private BlokkeringViewModel modelMetFout;

        [TestInitialize]
        public void SetUpContect()
        {
            DummyContext context = new DummyContext();
            mockGebruikersRepository = new Mock<IGebruikerRepository>();
            mockMateriaalRepository = new Mock<IMateriaalRepository>();
            personeel1 = context.personeel1;
            personeel1.BlokkeerMateriaal(context.dobbelsteen, 2, context.date, mockGebruikersRepository.Object);
            blokkatieController = new BlokkeringController(mockMateriaalRepository.Object, mockGebruikersRepository.Object);
            model = new BlokkeringViewModel()
            {
                Materiaal = context.wereldbol,
                aantal = 4,
                beginDatum = DateTime.Today.AddDays(23)
            };

            modelMetFout = new BlokkeringViewModel()
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
            ActionResult result = blokkatieController.Index(personeel1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void BlokkatieControllerRetourneertCorrectMaterialen()
        {
            ViewResult result = blokkatieController.Index(personeel1) as ViewResult;
            List<BlokkeringViewModel> models = (result.Model as IEnumerable<BlokkeringViewModel>).ToList();
            Assert.AreEqual("dobbelsteen", models[0].Materiaal.Artikelnaam);
            Assert.AreEqual(2, models[0].blokkering.Aantal);
        }

        [TestMethod]
        public void NieuwePostVoegtBlokkatieToe()
        {
            int aantal = personeel1.Blokkeringen.Count;
            blokkatieController.Nieuw(personeel1, model);
            mockGebruikersRepository.Verify(g => g.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void NieuwePostVerwijderdBlokkatie()
        {
            int aantal = personeel1.Blokkeringen.Count;
            blokkatieController.RemoveFromBlokkering(0, personeel1);
            Assert.AreEqual(aantal - 1, personeel1.Blokkeringen.Count);
            mockGebruikersRepository.Verify(g => g.SaveChanges(), Times.Once);
        }
    }
}

