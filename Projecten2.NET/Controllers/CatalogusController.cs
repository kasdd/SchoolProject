using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index(string searchString, int doelgroepId=0)
        {
            IEnumerable<Materiaal> materialen;
            Doelgroep doelgroep;

            if (doelgroepId == 0)
                materialen = materiaalRepository.FindAll().OrderBy(m => m.Artikelnaam).ToList();
            else
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    materialen =
                        materiaalRepository.FindAll()
                            .Where(
                                s =>
                                    s.Artikelnaam.ToLower().Contains(searchString.ToLower()) ||
                                    s.Omschrijving.ToLower().Contains(searchString.ToLower()));
                }
                else
                {
                    doelgroep = doelgroepRepository.FindById(doelgroepId);
                    materialen = doelgroep.Materialen.OrderBy(m => m.Artikelnaam);
                }
            }
            
            ViewBag.Doelgroep = getDoelgroepSelectList(doelgroepId);
            ViewBag.DoelgroepId = doelgroepId;

            return View(materialen);
        }

        private SelectList getDoelgroepSelectList(int selectedValue = 0)
        {
            return new SelectList(doelgroepRepository.FindAll().OrderBy(d=>d.DoelgroepNaam), "DoelgroepId", "DoelgroepNaam", selectedValue);

        }
    }
}