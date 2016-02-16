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

        public Materiaal findByArtikelNr(String nummer)
        {
            return materialen.Find(nummer);
        }

        public IQueryable<Materiaal> findAll()
        {
           return materialen;
        }

        public Materiaal findByName(string naam)
        {
            return materialen.Find(naam);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}