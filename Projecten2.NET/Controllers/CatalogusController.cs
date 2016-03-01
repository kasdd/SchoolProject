using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.Domain.IRepositories;

namespace Projecten2.NET.Controllers
{
    [Authorize]
   
    public class CatalogusController : Controller
    {
        private readonly IMateriaalRepository materiaalRepository;
        private readonly IDoelgroepRepository doelgroepRepository;

        public CatalogusController(IMateriaalRepository materiaalRepository, IDoelgroepRepository doelgroepRepository)
        {
            this.materiaalRepository = materiaalRepository;
            this.doelgroepRepository = doelgroepRepository;
        }
        // GET: Catalogus
        public ActionResult Index(string searchString, string doelgroep)
        {
            var DoelgroepLst = new List<String>();
            var DGQry = doelgroepRepository.FindAll().Select(n => n.DoelgroepNaam);

            
            DoelgroepLst.AddRange(DGQry);
            ViewBag.doelgroep = new SelectList(DoelgroepLst);
            
            IEnumerable<Materiaal> materialen = materiaalRepository.FindAll().OrderBy(m => m.Artikelnaam).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                materialen = materialen.Where(s => s.Artikelnaam.ToLower().Contains(searchString.ToLower()) || s.Omschrijving.ToLower().Contains(searchString.ToLower()));
                
            }

            if(!string.IsNullOrEmpty(doelgroep))
            {
                materialen = materialen.Where(dg => dg.Doelgroepen.Equals(doelgroep));

                //materialen = materialen.Where(dg => dg.Doelgroep == doelgroep);
            }

            return View(materialen);
        }

    }
}