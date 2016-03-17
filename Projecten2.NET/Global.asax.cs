using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Projecten2.NET.Infrastructuur;
using Projecten2.NET.Models;
using Projecten2.NET.Models.DAL;

namespace Projecten2.NET
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CatalogusContext db = new CatalogusContext();
            db.Database.Initialize(true);
            ModelBinders.Binders.Add(typeof(Gebruiker), new GebruikerModelBinder());
            ApplicationDbContext.Create();
        }
    }
}
