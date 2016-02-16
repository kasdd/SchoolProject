using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Projecten2.NET.Models.DAL.Mapper
{
    public class MateriaalMapper : EntityTypeConfiguration<Materiaal>
    {
        public MateriaalMapper()
        {
            ToTable("Materiaal");

            HasKey(m => m.ArtikelNummer);

            Property(m => m.Artikelnaam).IsRequired().HasMaxLength(100);
            Property(m => m.Doelgroep).HasMaxLength(100);
            Property(m => m.ArtikelNummer).IsRequired();
            Property(m => m.Prijs).IsRequired();
            Property(m => m.Uitleenbaar).IsRequired();

            HasOptional(r => r.Reservatie).WithRequired().Map(m => m.MapKey("ReservatieId"));
        }
    }
}