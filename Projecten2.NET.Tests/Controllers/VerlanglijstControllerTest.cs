using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2.NET.Controllers;
using Moq;
using Projecten2.NET.Models.Domain;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Projecten2.NET.Models.ViewModels;

namespace Projecten2.NET.Tests.Controllers
{
    [TestClass]
    public class VerlanglijstControllerTest
    {
        private VerlanglijstController verlanglijstController;
        private Mock<IMateriaalRepository> mockMateriaalRepository;
        private Mock<IGebruikerRepository> mockGebruikerRepository;
        private DummyContext context;
        private Gebruiker student1;
        private VerlanglijstViewModel model;

        [TestInitialize]
        public void MyTestInitializer()
        {
            context = new DummyContext();
            mockMateriaalRepository = new Mock<IMateriaalRepository>();
            mockGebruikerRepository = new Mock<IGebruikerRepository>();
            verlanglijstController = new VerlanglijstController(mockMateriaalRepository.Object, mockGebruikerRepository.Object);
            mockMateriaalRepository.Setup(m => m.FindByArtikelNaam("dobbelsteen"))
                .Returns(context.FindByArtikelNaam("dobbelsteen"));
            //Heeft al wereldbol in verlanglijst
            student1 = context.student1;
        }

        #region == Index ==

        [TestMethod]
        public void IndexRetourneertEenView()
        {
            ActionResult result = verlanglijstController.Index(student1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void VerlangLijstRetourneertCorrectMateriaal()
        {
            ViewResult result = verlanglijstController.Index(student1) as ViewResult;
            List<VerlanglijstViewModel> models = (result.Model as IEnumerable<VerlanglijstViewModel>).ToList();
            Assert.AreEqual("wereldbol", models[0].Artikelnaam);
        }
        [TestMethod]
        public void NieuwePostVoegtMateriaalToe()
        {
            int aantal = student1.Verlanglijst.Materialen.Count;
            verlanglijstController.AddToVerlanglijst("dobbelsteen", student1);
            Assert.AreEqual(aantal+1, student1.Verlanglijst.Materialen.Count);
            mockGebruikerRepository.Verify(g=>g.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void NieuwePostVoegtMateriaalNietToeBijFout()
        {
            int aantal = student1.Verlanglijst.Materialen.Count;
            verlanglijstController.AddToVerlanglijst(null, student1);
            Assert.AreEqual(aantal, student1.Verlanglijst.Materialen.Count);
            mockGebruikerRepository.Verify(g => g.SaveChanges(), Times.Never);
        }

       

        #endregion
    }
}
