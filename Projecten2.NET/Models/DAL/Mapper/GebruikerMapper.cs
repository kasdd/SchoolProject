using System.Data.Entity.ModelConfiguration;

namespace Projecten2.NET.Models.DAL.Mapper
{
    public class GebruikerMapper : EntityTypeConfiguration<Gebruiker>
    {
        public GebruikerMapper()
        {
            ToTable("Gebruiker");

            HasKey(g => g.GebruikerID);
            Property(g => g.Voornaam).IsRequired().HasMaxLength(100);
            Property(g => g.Naam).IsRequired().HasMaxLength(100);
            Property(g => g.Email).IsRequired().HasMaxLength(100);
            Property(g => g.Type).IsRequired().HasColumnName("Type");


            //HasRequired(g => g.Reservatie).WithRequiredPrincipal().WillCascadeOnDelete(false);
            HasRequired(g => g.Verlanglijst).WithRequiredPrincipal().WillCascadeOnDelete(false);
            //Geeft ModelValidationException! --> EF mapt ReservatieId vanzelf? 
            //HasMany(r => r.Reservaties).WithRequired().Map(m => m.MapKey("ReservatieId"));
        }
    }
}