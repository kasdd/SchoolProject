using System.Data.Entity.ModelConfiguration;

namespace Projecten2.NET.Models.DAL.Mapper
{
    public class ReservatieLijnMapper :EntityTypeConfiguration<ReservatieLijn>
    {
        public ReservatieLijnMapper()
        {
            ToTable("ReservatieLijn");
            HasKey(t => new { t.ReservatieId, t.MateriaalId });

            //HasRequired(l => l.Materiaal).WithMany().HasForeignKey(l => l.MateriaalId);
        }
    }
}