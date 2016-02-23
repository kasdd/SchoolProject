using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Projecten2.NET.Models.Domain;

namespace Projecten2.NET.Models.DAL
{
    public class MateriaalRepository : IMateriaalRepository
    {
        private CatalogusContext context;
        private DbSet<Materiaal> materialen;

        public MateriaalRepository(CatalogusContext context)
        {
            this.context = context;
            materialen = context.Materialen;
        }

        public Materiaal FindByArtikelNr(String nummer)
        {
            return materialen.Find(nummer);
        }

        public IQueryable<Materiaal> FindAll()
        {
           return materialen;
        }

        public Materiaal FindByName(string naam)
        {
            return materialen.Find(naam);
        }

        public void Add(Materiaal materiaal)
        {
            materialen.Add(materiaal);
        }

        public void Delete(Materiaal materiaal)
        {
            materialen.Remove(materiaal);
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}