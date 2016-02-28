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
        private readonly IMateriaalRepository materiaalRepository;

        public CatalogusController(IMateriaalRepository materiaalRepository)
        {
            this.materiaalRepository = materiaalRepository;
        }
        // GET: Catalogus
        public ActionResult Index(string searchString, string doelgroep)
        {
            var DoelgroepLst = new List<string>();
            var DGQry = materiaalRepository.FindAll().Select(dg => dg.Doelgroep);

            DoelgroepLst.AddRange(DGQry.Distinct());
            ViewBag.doelgroep = new SelectList(DoelgroepLst);
            
            IEnumerable<Materiaal> materialen = materiaalRepository.FindAll().OrderBy(m => m.Artikelnaam).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                materialen = materialen.Where(s => s.Artikelnaam.ToLower().Contains(searchString.ToLower()));
                
            }

            if(!string.IsNullOrEmpty(doelgroep))
            {
                materialen = materialen.Where(dg => dg.Doelgroep == doelgroep);
            }

            return View(materialen);
        }

    }
}