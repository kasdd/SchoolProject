using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecten2.NET.Models.Domain
{
    public interface IMateriaalRepository
    {
        Materiaal findByArtikelNr(String nummer);
        IQueryable<Materiaal> findAll();

        Materiaal findByName(String naam); 
        void SaveChanges();
    }
}
