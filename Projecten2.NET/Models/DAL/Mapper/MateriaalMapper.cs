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


            HasOptional(r => r.Reservatie).WithRequired().Map(m => m.MapKey("ReservatieId"));
        }
    }
}