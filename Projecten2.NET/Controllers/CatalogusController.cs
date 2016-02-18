using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projecten2.NET.Models.Domain;

namespace Projecten2.NET.Controllers
{
    [Authorize]
    public class CatalogusController : Controller
    {
        private IMateriaalRepository materiaalRepository;

        public CatalogusController(IMateriaalRepository materiaalRepository)
        {
            this.materiaalRepository = materiaalRepository;
        }
        // GET: Catalogus
        public ActionResult Index(Gebruiker gebruiker)
        {
            IEnumerable<Materiaal> materialen = gebruiker.GeefCorrecteCatalogus(gebruiker);
            return View(materialen);
        }

    }
}