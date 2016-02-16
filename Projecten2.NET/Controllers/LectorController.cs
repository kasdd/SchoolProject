using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models;

namespace Projecten2.NET.Controllers
{
    [Authorize(Roles = "Lector")]
    public class LectorController : Controller
    {
        IGebruikerRepository gebruikerRepository;
        //
        // GET: Lector
        public LectorController(IGebruikerRepository repo)
        {
            gebruikerRepository = repo;

        }
        
        //
        // GET: /Home/Index
        public ActionResult Index(Gebruiker gebruiker)
        {
            return View("Index");
        }
    }
}