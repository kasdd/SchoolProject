using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projecten2.NET.Controllers;
using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.Domain.IRepositories;
using Moq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Projecten2.NET.Models.ViewModels;

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
        private Gebruiker student1;
        private CatalogusViewModel model;
        private CatalogusViewModel modelMetFout;
        private DummyContext context;
        [TestInitialize]
        public void MyTestInitializer()
        {
            context = new DummyContext();
            mockMateriaalRepository = new Mock<IMateriaalRepository>();
            mockDoelgroepRepository = new Mock<IDoelgroepRepository>();
            mockLeergebiedRepository = new Mock<ILeergebiedRepository>();
            mockGebruikerRepository = new Mock<IGebruikerRepository>();
            student1 = context.student1;
            mockMateriaalRepository.Setup(m => m.FindAll()).Returns(context.AllMaterialen);
            mockDoelgroepRepository.Setup(d => d.FindById(1)).Returns(context.FindByIdDoelgroep(1));
            mockLeergebiedRepository.Setup(l => l.FindById(1)).Returns(context.FindByIdLeergebied(1));
            mockDoelgroepRepository.Setup(d => d.FindById(3)).Returns(context.FindByIdDoelgroep(3));
            catalogusController = new CatalogusController(mockMateriaalRepository.Object, mockDoelgroepRepository.Object, mockLeergebiedRepository.Object, mockGebruikerRepository.Object);
            
            
        }

        [TestMethod]
        public void IndexRetourneertEenView()
        {
            ActionResult result = catalogusController.Index(student1, "");
            Assert.IsInstanceOfType(result, typeof (ViewResult));
        }

        [TestMethod]
        public void IndexRetourneertAlleMaterialen()
        {
            ViewResult result = catalogusController.Index(student1, "") as ViewResult;
            List<CatalogusViewModel> models = (result.Model as IEnumerable<CatalogusViewModel>).ToList();
            Assert.AreEqual(3, models.Count);
        }

        [TestMethod]
        public void IndexRetrouneertCorrectMateriaal()
        {
            model = new CatalogusViewModel(student1, context.dobbelsteen);
            ViewResult result = catalogusController.Index(student1, "") as ViewResult;
            Assert.AreEqual("dobbelsteen", model.Artikelnaam);
        }

        [TestMethod]
        public void IndexRetourneertAlleMaterialenMetDoelgroepId()
        {
            ViewResult result = catalogusController.Index(student1, "", 1) as ViewResult;
            List<CatalogusViewModel> models = (result.Model as IEnumerable<CatalogusViewModel>).ToList();
            Assert.AreEqual(2, models.Count);
            Assert.AreEqual("frozen spelbord", models[0].Artikelnaam);
            Assert.AreEqual("wereldbol", models[1].Artikelnaam);
        }

        [TestMethod]
        public void IndexRetourneertAlleMaterialenMetLeergebiedId()
        {
            ViewResult result = catalogusController.Index(student1, "", 0, 1) as ViewResult;
            List<CatalogusViewModel> models = (result.Model as IEnumerable<CatalogusViewModel>).ToList();
            Assert.AreEqual(1, models.Count);
            Assert.AreEqual("frozen spelbord", models[0].Artikelnaam);
        }

        //[TestMethod]
        //public void IndexRetourneertGeldigeString()
        //{
        //    ViewResult result = catalogusController.Index(student1, "mooie") as ViewResult;
        //    List<CatalogusViewModel> models = (result.Model as IEnumerable<CatalogusViewModel>).ToList();
        //    Assert.AreEqual(1, models.Count);
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
