using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2.NET.Controllers;
using Moq;
using Projecten2.NET.Models.Domain;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Projecten2.NET.Tests.Controllers
{
    [TestClass]
    public class VerlanglijstControllerTest
    {
        private VerlanglijstController verlanglijstController;
        private Mock<IMateriaalRepository> mockMateriaalRepository;
        private Mock<IGebruikerRepository> mockGebruikerRepository;
        private readonly DummyContext dummyContext = new DummyContext();

        [TestInitialize]
        public void MyTestInitializer()
        {
            mockMateriaalRepository = new Mock<IMateriaalRepository>();
            mockGebruikerRepository = new Mock<IGebruikerRepository>();
            mockMateriaalRepository.Setup(p => p.FindAll()).Returns(dummyContext.AllMaterialen);
            mockMateriaalRepository.Setup(p => p.FindByArtikelNaam("Blanco draaischijf")).Returns(dummyContext.FindByArtikelNaamMaterialen("Blanco draaischijf"));
            mockMateriaalRepository.Setup(p => p.FindByArtikelNaam("Splitsbomen")).Returns(dummyContext.FindByArtikelNaamMaterialen("Splitsbomen"));
            verlanglijstController = new VerlanglijstController(mockMateriaalRepository.Object, mockGebruikerRepository.Object);
        }

        #region == Index ==

        [TestMethod]
        public void IndexRetourneertVerlanglijst()
        {
            //ViewResult result = VerlanglijstController.Index("") as ViewResult;
            //List<Materiaal> materialen = (result.Model as IEnumerable<Materiaal>).ToList();
            //Assert.AreEqual(2, materialen.Count);
            //Assert.AreEqual("dobbelsteen", materialen[0].Artikelnaam);
            //Assert.AreEqual("wereldbol", materialen[1].Artikelnaam);
        }

        [TestMethod]
        public void IndexRetourneertAlleMaterialenInKleuters()
        {
            //ViewResult result = VerlanglijstController.Index("", 11, 0) as ViewResult;
            //List<Materiaal> materialen = (result.Model as IEnumerable<Materiaal>).ToList();
            //Assert.AreEqual(1, materialen.Count);
            //Assert.AreEqual("wereldbol", materialen[0].Artikelnaam);
        }
        [TestMethod]
        public void IndexRetourneertOngeldigeStringMaterialenInKleuters()
        {
            //ViewResult result = verlanglijstController.Index("qdmfqmsfjkq", 11, 0) as ViewResult;
            //List<Materiaal> materialen = (result.Model as IEnumerable<Materiaal>).ToList();
            //Assert.AreEqual(0, materialen.Count);
        }

        #endregion
    }
}
