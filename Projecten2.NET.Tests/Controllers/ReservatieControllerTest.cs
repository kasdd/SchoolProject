using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Projecten2.NET.Controllers;
using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.ViewModels;

namespace Projecten2.NET.Tests.Controllers
{
    [TestClass]
    public class ReservatieControllerTest
    {
        private ReservatieController controller;
        private Gebruiker student1;
        private Mock<IGebruikerRepository> mockGebruikersRepository;
        private Mock<IMateriaalRepository> mockMateriaalRepository;
        private NieuweReservatieViewModel model;
        private NieuweReservatieViewModel modelMetFout;

        [TestInitialize]
        public void SetUpContect()
        {
            DummyContext context = new DummyContext();
            mockGebruikersRepository = new Mock<IGebruikerRepository>();
            mockMateriaalRepository = new Mock<IMateriaalRepository>();
            student1 = context.student1;
            controller = new ReservatieController(mockMateriaalRepository.Object, mockGebruikersRepository.Object);
            model = new NieuweReservatieViewModel(context.wereldbol)
            {
                aantal = 4,
                beginDatum = DateTime.Today.AddDays(23)
            };

            modelMetFout = new NieuweReservatieViewModel(context.wereldbol)
            {
                aantal = 12,
                beginDatum = DateTime.Today
            };
        }

        [TestMethod]
        public void blabla()
        {
            
        }
        

    }
}
