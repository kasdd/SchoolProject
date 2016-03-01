using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Projecten2.NET.Models.Domain;
using IModelBinder = System.Web.Mvc.IModelBinder;

namespace Projecten2.NET.Models.DAL.Infrastructuur
{
    public class GebruikerModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                IGebruikerRepository repo =
                    (IGebruikerRepository) DependencyResolver.Current.GetService(typeof (IGebruikerRepository));
                return repo.FindByEmail(controllerContext.HttpContext.User.Identity.Name);
            }
            return null;
        }
    }

}
