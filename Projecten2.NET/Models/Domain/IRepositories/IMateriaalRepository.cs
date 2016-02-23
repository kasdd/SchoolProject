﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecten2.NET.Models.Domain
{
    public interface IMateriaalRepository
    {
        Materiaal FindByArtikelNr(String nummer);
        IQueryable<Materiaal> FindAll();

        Materiaal FindByName(String naam);

        void Add(Materiaal materiaal);

        void Delete(Materiaal materiaal);
        void SaveChanges();
    }
}
