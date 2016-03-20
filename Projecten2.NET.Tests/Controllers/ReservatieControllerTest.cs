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
        private VoorbehoudingViewModel model;
        private VoorbehoudingViewModel modelMetFout;

        [TestInitialize]
        public void SetUpContect()
        {
            DummyContext context = new DummyContext();
            mockGebruikersRepository = new Mock<IGebruikerRepository>();
            mockMateriaalRepository = new Mock<IMateriaalRepository>();
            student1 = context.student1;
            reservatieController = new ReservatieController(mockMateriaalRepository.Object, mockGebruikersRepository.Object);
            model = new VoorbehoudingViewModel(context.wereldbol)
            {
                aantal = 4,
                beginDatum = DateTime.Today.AddDays(23)
            };

            modelMetFout = new VoorbehoudingViewModel(context.wereldbol)
            {
                aantal = 12,
                beginDatum = DateTime.Today
            };
        }
        [TestMethod]
        public void IndexRetourneertEenView()
        {
            ActionResult result = reservatieController.Index(student1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        public void ReservatieControllerRetourneertCorrectMaterialen()
        {
            ViewResult result = reservatieController.Index(student1) as ViewResult;
            List<ReservatieViewModel> models = (result.Model as IEnumerable<ReservatieViewModel>).ToList();
   //         Assert.AreEqual("dobbeslteen", models[0].Artikelnaam);
            Assert.AreEqual(3, models[0].aantal);
        }

        //public void NieuwePostReserveertMateriaal()
        //{
        //    int aantal = student1.Verlanglijst.Materialen.Count;
        //    reservatieController.Nieuw("dobbelsteen", student1);
        //    Assert.AreEqual(aantal + 1, student1.Verlanglijst.Materialen.Count);
        //    mockGebruikerRepository.Verify(g => g.SaveChanges(), Times.Once);
        //}

        //[TestMethod]
        //public void NieuwePostVoegtMateriaalNietToeBijFout()
        //{
        //    int aantal = student1.Verlanglijst.Materialen.Count;
        //    verlanglijstController.AddToVerlanglijst(null, student1);
        //    Assert.AreEqual(aantal, student1.Verlanglijst.Materialen.Count);
        //    mockGebruikerRepository.Verify(g => g.SaveChanges(), Times.Never);
        //}

    }
}
