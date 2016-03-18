using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2.NET.Controllers;
using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.Domain.IRepositories;
using Moq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Projecten2.NET.Tests.Controllers
{
    [TestClass]
    public class CatalogusControllerTest
    {
        private CatalogusController catalogusController;
        private Mock<IMateriaalRepository> mockMateriaalRepository;
        private Mock<IDoelgroepRepository> mockDoelgroepRepository;
        private Mock<ILeergebiedRepository> mockLeergebiedRepository;
        private Mock<IGebruikerRepository> mockGebruikerRepository;
        public Gebruiker student1;

        [TestInitialize]
        public void MyTestInitializer()
        {
            DummyContext context = new DummyContext();
            mockMateriaalRepository = new Mock<IMateriaalRepository>();
            mockDoelgroepRepository = new Mock<IDoelgroepRepository>();
            mockLeergebiedRepository = new Mock<ILeergebiedRepository>();
            mockGebruikerRepository = new Mock<IGebruikerRepository>();
            mockMateriaalRepository.Setup(p => p.FindAll()).Returns(context.AllMaterialen);
            mockMateriaalRepository.Setup(p => p.FindByArtikelNaam("Blanco draaischijf")).Returns(context.FindByArtikelNaamMaterialen("Blanco draaischijf"));
            mockMateriaalRepository.Setup(p => p.FindByArtikelNaam("Splitsbomen")).Returns(context.FindByArtikelNaamMaterialen("Splitsbomen"));
            mockDoelgroepRepository.Setup(p => p.FindAll()).Returns(context.AllDoelgroepen);
            mockDoelgroepRepository.Setup(p => p.FindById(11)).Returns(context.FindByIdDoelgroep(11));
            mockDoelgroepRepository.Setup(p => p.FindById(12)).Returns(context.FindByIdDoelgroep(12));
            catalogusController = new CatalogusController(mockMateriaalRepository.Object, mockDoelgroepRepository.Object, mockLeergebiedRepository.Object, mockGebruikerRepository.Object);
            student1 = context.student1;

        }

        [TestMethod]
        public void IndexRetourneertAlleMaterialen()
        {
            ViewResult result = catalogusController.Index(student1, "") as ViewResult;
            List<Materiaal> materialen = (result.Model as IEnumerable<Materiaal>).ToList();
            Assert.AreEqual(2, materialen.Count);
            Assert.AreEqual("dobbelsteen", materialen[0].Artikelnaam);
            Assert.AreEqual("wereldbol", materialen[1].Artikelnaam);
        }

        //[TestMethod]
        //public void IndexRetourneertAlleMaterialenInKleuters()
        //{
        //    ViewResult result = catalogusController.Index("", 11, 0) as ViewResult;
        //    List<Materiaal> materialen = (result.Model as IEnumerable<Materiaal>).ToList();
        //    Assert.AreEqual(1, materialen.Count);
        //    Assert.AreEqual("wereldbol", materialen[0].Artikelnaam);
        //}
        //[TestMethod]
        //public void IndexRetourneertOngeldigeStringMaterialenInKleuters()
        //{
        //    ViewResult result = catalogusController.Index("qdmfqmsfjkq", 11, 0) as ViewResult;
        //    List<Materiaal> materialen = (result.Model as IEnumerable<Materiaal>).ToList();
        //    Assert.AreEqual(0, materialen.Count);
        //}

    }
}
