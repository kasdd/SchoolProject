using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Projecten2.NET.Models.Domain;
using Projecten2.NET.Models.Domain.IRepositories;

namespace Projecten2.NET.Models.DAL
{
    public class BlokkeringRepository : IBlokkeringRepository
    {

        private CatalogusContext context;
        private DbSet<Blokkering> blokkeringen;

        public BlokkeringRepository(CatalogusContext context)
        {
            this.context = context;
            blokkeringen = context.Blokkeringen;
        }
        public IQueryable<Blokkering> getbyDate(DateTime datum)
        {
            return blokkeringen.Where(r => r.BeginDate == datum);
        }

        public IQueryable<Blokkering> FindAll()
        {
            return blokkeringen;
        }

        public void AddBlokkering(Blokkering b)
        {
            blokkeringen.Add(b);
        }

        public void RemoveBlokkering(Blokkering b)
        {
            blokkeringen.Remove(b);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}