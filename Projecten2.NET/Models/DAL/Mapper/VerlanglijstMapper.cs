using System.Data.Entity.ModelConfiguration;

namespace Projecten2.NET.Models.DAL.Mapper
{
    public class VerlanglijstMapper: EntityTypeConfiguration<Verlanglijst>
    {
        public VerlanglijstMapper()
        {
           /* ToTable("VerlangLijst");
            HasKey(v => v.VerlanglijstId);

            HasMany(l => l.Materialen).WithOptional().Map(l => l.MapKey("MateriaalId"));*/
        }
    }
}