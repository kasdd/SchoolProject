using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Projecten2.NET.Models.DAL.Mapper
{
    public class GebruikerMapper : EntityTypeConfiguration<Gebruiker>
    {
        public GebruikerMapper()
        {
            ToTable("Gebruiker");

            HasKey(g => g.GebruikerID);

            Property(g => g.Loginnaam).IsRequired();
            Property(g => g.Wachtwoord).IsRequired();
            Property(g => g.Voornaam).IsRequired().HasMaxLength(100);
            Property(g => g.Naam).IsRequired().HasMaxLength(100);
            Property(g => g.Email).IsRequired().HasMaxLength(100);

            HasMany(r => r.Reservaties).WithRequired().Map(m => m.MapKey("GebruikersId"));
            HasMany(m => m.Materialen).WithOptional().Map(m => m.MapKey("GebruikersId"));
        }
    }
}