using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Projecten2.NET.Models.Domain.IRepositories;

namespace Projecten2.NET.Models.DAL
{
    public class LeergebiedRepository : ILeergebiedRepository
    {

        private CatalogusContext context;
        private DbSet<Leergebied> leergebieden;

        public LeergebiedRepository(CatalogusContext context)
        {
            this.context = context;
            leergebieden = context.Leergebieden;
        }

        public Leergebied FindById(int Id)
        {
            return leergebieden.Find(Id);
        }

        public IQueryable<Leergebied> FindAll()
        {
            return leergebieden;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void AddLeergebied(Leergebied leergebied)
        {
            leergebieden.Add(leergebied);
        }
    }
}