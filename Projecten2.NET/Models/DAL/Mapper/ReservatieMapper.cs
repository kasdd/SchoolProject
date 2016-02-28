using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Projecten2.NET.Models.DAL.Mapper
{
    public class ReservatieMapper : EntityTypeConfiguration<Reservatie>
    {
        public ReservatieMapper()
        {
            ToTable("Reservatie");
            HasKey(r => r.ReservatieId);

            HasMany(r => r.Materialen).WithOptional().Map(m => m.MapKey("MateriaalId"));
        }
    }
}