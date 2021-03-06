﻿using System;
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
        public Materiaal FindByArtikelNaam(String naam)
        {
            return materialen.FirstOrDefault(m => m.Artikelnaam == naam);
        }
        public IQueryable<Materiaal> FindAll()
        {
           return materialen;
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}