using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Projecten2.NET.Controllers;
using Projecten2.NET.Models;
using Projecten2.NET.Models.DAL;

namespace Projecten2.NET
{
    
    public class Verlanglijstje
    { 
         #region Properties
        
        CatalogusContext db = new CatalogusContext();
        private string VerlanglijstjeId { get; set; }
        #endregion

        #region Methods
      public static Verlanglijstje GetVerlanglijstje( HttpContextBase context)
        {
            var lijst = new Verlanglijstje();
            lijst.VerlanglijstjeId = lijst.GetUser(context);
            return lijst;
           
        }

        public Verlanglijstje GetVerlanglijstje(Controller controller)
        {
            return GetVerlanglijstje(controller.HttpContext);
        }

        private string GetUser(HttpContextBase context)
        {
            if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
            {
                    return context.User.Identity.Name;
            }
            else
            {
                // Generate a new random GUID using System.Guid class
                Guid tempCartId = Guid.NewGuid();
               return tempCartId.ToString();
            }
        }

        public void AddToVerlanglijstje(Materiaal materiaal)
        {
            var item = db.Lijst.SingleOrDefault(v => v.LijstId == VerlanglijstjeId
            && v.MateriaalId == materiaal.MateriaalId);

            if (item == null)
            {
                item = new Lijst
                {
                    MateriaalId = materiaal.MateriaalId,
                    LijstId = VerlanglijstjeId,
                    Aantal = 1,
                    Aangemaakt = DateTime.Now
                };
                db.Lijst.Add(item);
            }
            else
            {
                //Meerdere van hetzelfde item
                item.Aantal++;
            }
            db.SaveChanges();
     
        }

        public int RemoveFromVerlanglijstje(int id)
        {
            var item = db.Lijst.Single(v => v.LijstId == VerlanglijstjeId
                                            && v.MateriaalId == id);
            int itemAantal = 0;
            if (item != null)
            {
                if (item.Aantal > 1)
                {
                    item.MateriaalId--;
                    itemAantal = item.Aantal;
                }
                else
                {
                    db.Lijst.Remove(item);
                }
                db.SaveChanges();
            }
            return itemAantal;
        }

        public void EmptyAllVerlanglijstje()
        {
            var items = db.Lijst.Where(v => v.LijstId == VerlanglijstjeId);

            foreach (var item in items)
            {
                db.Lijst.Remove(item);
            }
            db.SaveChanges();
        }

        public List<Lijst> GetVerlanglijstjeItems()
        {
           return db.Lijst.Where(v => v.LijstId == VerlanglijstjeId).ToList();
        }

        public int GetAantal()
        {
            //Aantal items in het verlanglijstje & de som
             int? aantal = (from items in db.Lijst
                where items.LijstId  == VerlanglijstjeId
                select (int?)items.Aantal).Sum();
            //Aantal wordt nul gezet indien items == null
            return aantal ?? 0;
        }

        public double GetTotaal()
        {
            //prijzen optellen & prijs vereenvoudigd
            double? totaal = (from items in db.Lijst
                where items.LijstId == VerlanglijstjeId
                select (int?) items.Aantal
                * items.Materiaal.Prijs).Sum();
            //totaal wordt op 0 gezet indien er geen items zijn.
            return totaal ?? 0;

        }

        public void CombineerLijstUser(string mail)
                {
                    var verlanglijst = db.Lijst.Where(v => v.LijstId == VerlanglijstjeId);

                    foreach (Lijst item in verlanglijst)
                    {
                        item.LijstId = mail;
                    }
                    db.SaveChanges();
                }

        #endregion
    }
}