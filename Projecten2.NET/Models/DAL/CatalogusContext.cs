using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace Projecten2.NET.Models.DAL
{
    public class CatalogusContext : DbContext
    {

        public CatalogusContext() : base("Catalogus")
        {
        }

        public DbSet<Doelgroep> Doelgroepen { get; set; }
        public DbSet<Leergebied> Leergebieden { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Materiaal> Materialen { get; set; }
        public DbSet<Reservatie> Reservaties { get; set; } 
            
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}