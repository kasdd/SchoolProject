using System;
using System.Data.Entity;
using System.Linq;
using Projecten2.NET.Models.Domain;

namespace Projecten2.NET.Models.DAL
{
    public class GebruikersRepository : IGebruikerRepository
    {
        private CatalogusContext context;
        private DbSet<Gebruiker> gebruikers;

        public GebruikersRepository(CatalogusContext cxt)
        {
            context = cxt;
            gebruikers = cxt.Gebruikers;
        }

        public Gebruiker FindByEmail(string email)
        {
            return gebruikers.FirstOrDefault(g => g.Email == email);
        }

        public IQueryable<Gebruiker> FindAll()
        {
            return gebruikers;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public Gebruiker FindByName(string name)
        {
            return gebruikers.Find(name);
        }

        public void AddGebruiker(Gebruiker gebruiker)
        {
            gebruikers.Add(gebruiker);
        }
    }
}