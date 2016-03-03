using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2.NET.Controllers;
using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.Domain.IRepositories;
using Moq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

/*namespace Projecten2.NET.Tests.Controllers
{
   [TestClass]
    public class CatalogusControllerTest
    {
        private CatalogusController catalogusController;
        private Mock<IMateriaalRepository> mockMateriaalRepository;
        private Mock<IDoelgroepRepository> mockDoelgroepRepository;
        private readonly DummyContext dummyContext = new DummyContext();

        [TestInitialize]
        public void MyTestInitializer()
        {
            mockMateriaalRepository = new Mock<IMateriaalRepository>();
            mockDoelgroepRepository = new Mock<IDoelgroepRepository>();
         //   runningShoes = dummyContext.RunningShoes;
         //   runningShoesId = runningShoes.ProductId;
            mockMateriaalRepository.Setup(p => p.FindAll()).Returns(dummyContext.AllMaterialen);
            mockMateriaalRepository.Setup(p => p.FindByArtikelNr("101")).Returns(dummyContext.FindByArtikelNrMaterialen("101"));
            mockMateriaalRepository.Setup(p => p.FindByArtikelNr("102")).Returns(dummyContext.FindByArtikelNrMaterialen("102"));
            mockDoelgroepRepository.Setup(p => p.FindAll()).Returns(dummyContext.AllDoelgroepen);
            mockDoelgroepRepository.Setup(p => p.FindById(11)).Returns(dummyContext.FindByIdDoelgroep(11));
            mockDoelgroepRepository.Setup(p => p.FindById(12)).Returns(dummyContext.FindByIdDoelgroep(12));
        }

        #region == Index ==

        [TestMethod]
        public void IndexRetourneertAlleMaterialen()
        {
            ViewResult result = catalogusController.Index("", "" ) as ViewResult;
            List<Materiaal> materialen = (result.Model as IEnumerable<Materiaal>).ToList();
            Assert.AreEqual(2, materialen.Count);
            Assert.AreEqual("wereldbol", materialen[0].Artikelnaam);
            Assert.AreEqual("dobbelsteen", materialen[1].Artikelnaam);
        }

        [TestMethod]
        public void IndexRetourneertAlleMaterialenInKleuters()
        {
            ViewResult result = catalogusController.Index("", "kleuters") as ViewResult;
            List<Materiaal> materialen = (result.Model as IEnumerable<Materiaal>).ToList();
            Assert.AreEqual(1, materialen.Count);
            Assert.AreEqual("wereldbol", materialen[0].Artikelnaam);
        }
        [TestMethod]
        public void IndexRetourneertOngeldigeStringMaterialenInKleuters()
        {
            ViewResult result = catalogusController.Index("qdmfqmsfjkq", "kleuters") as ViewResult;
            List<Materiaal> materialen = (result.Model as IEnumerable<Materiaal>).ToList();
            Assert.AreEqual(0, materialen.Count);
        }

        #endregion
    }
}*/
