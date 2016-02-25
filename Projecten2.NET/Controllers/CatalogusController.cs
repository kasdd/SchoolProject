using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projecten2.NET.Models.Domain;

namespace Projecten2.NET.Controllers
{
   
    public class CatalogusController : Controller
    {
        private readonly IMateriaalRepository materiaalRepository;

        public CatalogusController(IMateriaalRepository materiaalRepository)
        {
            this.materiaalRepository = materiaalRepository;
        }
        // GET: Catalogus
        public ActionResult Index()
        {
            IEnumerable<Materiaal> materialen= materiaalRepository.FindAll().OrderBy(m=>m.Artikelnaam).ToList();
            //IEnumerable<Materiaal> materialen = gebruiker.GeefCorrecteCatalogus(gebruiker);

            return View(materialen);
        }

    }
}