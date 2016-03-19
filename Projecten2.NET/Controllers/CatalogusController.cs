using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.Domain.IRepositories;
using Projecten2.NET.Models.ViewModels;

namespace Projecten2.NET.Controllers
{
    [Authorize]
    public class CatalogusController : Controller
    {
        private readonly IMateriaalRepository materiaalRepository;
        private readonly IDoelgroepRepository doelgroepRepository;
        private readonly ILeergebiedRepository leergebiedRepository;
        private readonly IGebruikerRepository gebruikerRepository;

        public CatalogusController(IMateriaalRepository materiaalRepository, IDoelgroepRepository doelgroepRepository, ILeergebiedRepository leergebiedRepository, IGebruikerRepository gebruikerRepository)
        {
            this.materiaalRepository = materiaalRepository;
            this.doelgroepRepository = doelgroepRepository;
            this.leergebiedRepository = leergebiedRepository;
            this.gebruikerRepository = gebruikerRepository;
        }
        // GET: Catalogus
        public ActionResult Index(Gebruiker gebruiker, string searchString, int doelgroepId=0, int leergebiedId=0)
        {
            try
            {
                IEnumerable<Materiaal> materialen;
                Doelgroep doelgroep;
                Leergebied leergebied;
                if (doelgroepId != 0)
                {
                    doelgroep = doelgroepRepository.FindById(doelgroepId);
                    materialen = doelgroep.Materialen.OrderBy(m => m.Artikelnaam);
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        materialen.Where(s =>
                            s.Artikelnaam.ToLower().Contains(searchString.ToLower()) ||
                            s.Omschrijving.ToLower().Contains(searchString.ToLower()));
                    }
                    ViewBag.Doelgroep = getDoelgroepSelectList(doelgroepId);
                    ViewBag.DoelgroepId = doelgroepId;
                    ViewBag.Leergebied = getLeergebiedSelectedList(leergebiedId);
                    ViewBag.LeergebiedId = leergebiedId;
                    return View(materialen.Select(m => new CatalogusViewModel(gebruiker, m)));
                }
                if (leergebiedId != 0)
                {
                    leergebied = leergebiedRepository.FindById(leergebiedId);
                    materialen = leergebied.Materialen.OrderBy(m => m.ArtikelNummer);
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        materialen.Where(s =>
                            s.Artikelnaam.ToLower().Contains(searchString.ToLower()) ||
                            s.Omschrijving.ToLower().Contains(searchString.ToLower()));
                    }
                    ViewBag.Doelgroep = getDoelgroepSelectList(doelgroepId);
                    ViewBag.DoelgroepId = doelgroepId;
                    ViewBag.Leergebied = getLeergebiedSelectedList(leergebiedId);
                    ViewBag.LeergebiedId = leergebiedId;
                    return View(materialen.Select(m => new CatalogusViewModel(gebruiker, m)));

                }
                if (!string.IsNullOrEmpty(searchString))
                {
                    materialen = materiaalRepository.FindAll()
                            .Where(
                                s =>
                                    s.Artikelnaam.ToLower().Contains(searchString.ToLower()) ||
                                    s.Omschrijving.ToLower().Contains(searchString.ToLower()));
                }
                else
                {
                    materialen = materiaalRepository.FindAll().OrderBy(m => m.Artikelnaam).ToList();
                }

                ViewBag.Doelgroep = getDoelgroepSelectList(doelgroepId);
                ViewBag.DoelgroepId = doelgroepId;
                ViewBag.Leergebied = getLeergebiedSelectedList(leergebiedId);
                ViewBag.LeergebiedId = leergebiedId;
                return View(materialen.Select(m => new CatalogusViewModel(gebruiker, m)));
            }
            catch (Exception)
            {
                TempData["error"] = $"Er kunnen geen didactische leermiddelen worden opgehaald";
            }
            return View();
        }

        private SelectList getDoelgroepSelectList(int selectedValue = 0)
        {
            return new SelectList(doelgroepRepository.FindAll().OrderBy(d=>d.DoelgroepNaam), "DoelgroepId", "DoelgroepNaam", selectedValue);

        }

        private SelectList getLeergebiedSelectedList(int selectedValue = 0)
        {
            return new SelectList(leergebiedRepository.FindAll().OrderBy(l=>l.LeergebiedNaam),"LeergebiedId", "LeergebiedNaam", selectedValue);
        }
    }
}