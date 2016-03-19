using System.Web.Mvc;
using Projecten2.NET.Models.Domain;
using IModelBinder = System.Web.Mvc.IModelBinder;

namespace Projecten2.NET.Infrastructuur
{
    public class GebruikerModelBinder : IModelBinder
    {

        private const string gebruikerSessionKey = "gebruiker";
        private IGebruikerRepository repo;
        private Gebruiker gebruiker;
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                gebruiker = controllerContext.HttpContext.Session[gebruikerSessionKey] as Gebruiker;

                if (gebruiker == null)
                {
                    repo = (IGebruikerRepository)DependencyResolver.Current.GetService(typeof(IGebruikerRepository));
                    gebruiker = repo.FindByEmail(controllerContext.HttpContext.User.Identity.Name);
                    controllerContext.HttpContext.Session[gebruikerSessionKey] = gebruiker;
                }
                if (gebruiker.Email != controllerContext.HttpContext.User.Identity.Name)
                {
                    gebruiker = repo.FindByEmail(controllerContext.HttpContext.User.Identity.Name);
                    controllerContext.HttpContext.Session[gebruikerSessionKey] = gebruiker;
                }
                return gebruiker;
            }
            return null;

        }

    }

}