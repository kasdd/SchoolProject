using System.Web.Mvc;
using Projecten2.NET.Models.Domain;
using IModelBinder = System.Web.Mvc.IModelBinder;

namespace Projecten2.NET.Infrastructuur
{
    public class GebruikerModelBinder : IModelBinder
    {
        private const string gebruikerSessionKey = "gebruiker";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {

             if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                Gebruiker gebruiker = controllerContext.HttpContext.Session[gebruikerSessionKey] as Gebruiker;
                if (gebruiker == null)
                {
                    IGebruikerRepository repo =
                    (IGebruikerRepository)DependencyResolver.Current.GetService(typeof(IGebruikerRepository));
                    gebruiker = repo.FindByEmail(controllerContext.HttpContext.User.Identity.Name);
                    controllerContext.HttpContext.Session[gebruikerSessionKey] = gebruiker;
                }
                return gebruiker;
            }

            return null;

        }

    }

}
