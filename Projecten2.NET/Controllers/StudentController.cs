using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projecten2.NET.Models.DAL;
using Projecten2.NET.Models.Domain;

namespace Projecten2.NET.Controllers
{
    [Authorize(Roles = "student")]
    public class StudentController : Controller

    {
        private IGebruikerRepository gebruikerRepository;
        // GET: Promotor
        public StudentController(GebruikersRepository repo)
        {
            gebruikerRepository = repo;

        }

        // GET : Home/Index
        public ActionResult Index(Gebruiker gebruiker)
        {
            return View("Index");
        }

    }
}