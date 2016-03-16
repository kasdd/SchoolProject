using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Projecten2.NET.Models.Domain.IRepositories;

namespace Projecten2.NET.Models.DAL.Mapper
{
    public class ReservatieRepository : IReservatieRepository
    {
        private CatalogusContext context;
        private DbSet<Reservatie> reservaties;

        public ReservatieRepository(CatalogusContext context)
        {
            this.context = context;
            reservaties = context.Reservaties;
        }
        public IQueryable<Reservatie> getbyDate(DateTime datum)
        {
            return reservaties.Where(r => r.BeginDate == datum);
        }
    }
}