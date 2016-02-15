using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Projecten2.NET.Models.DAL
{
    public class CatalogusContext : IdentityDbContext<ApplicationUser>
    {

        public CatalogusContext() : base("Catalogus")
        {
            
        }

        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Reservatie> Reservaties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static CatalogusContext Create()
        {
            return DependencyResolver.Current.GetService<CatalogusContext>();
        }

    }
}