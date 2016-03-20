using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecten2.NET.Models.Domain
{
    public interface IGebruikerRepository
    {
        Gebruiker FindByEmail(string email);
        IQueryable<Gebruiker> FindAll();
        void SaveChanges();
        Gebruiker FindById(int id);
        void AddGebruiker(Gebruiker gebruiker);

    }
}
