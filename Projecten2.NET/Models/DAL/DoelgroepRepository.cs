using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Projecten2.NET.Models.Domain.IRepositories;

namespace Projecten2.NET.Models.DAL
{
    public class DoelgroepRepository : IDoelgroepRepository
    {
        private CatalogusContext context;
        private DbSet<Doelgroep> doelgroepen;

        public DoelgroepRepository(CatalogusContext context)
        {
            this.context = context;
            this.doelgroepen = context.Doelgroepen;
        }
        public void AddDoelgroep(Doelgroep doelgroep)
        {
            doelgroepen.Add(doelgroep);
        }

        public IQueryable<Doelgroep> FindAll()
        {
            return doelgroepen;
        }

        public Doelgroep FindById(int id)
        {
            return doelgroepen.Find(id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}