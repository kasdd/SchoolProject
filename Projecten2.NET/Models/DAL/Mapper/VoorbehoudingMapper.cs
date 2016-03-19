using Projecten2.NET.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Projecten2.NET.Models.DAL.Mapper
{
    public class VoorbehoudingMapper : EntityTypeConfiguration<Voorbehouding>
    {
        public VoorbehoudingMapper() {

            Map<Reservatie>(m => m.Requires("Type").HasValue("RESERVATIE"));
            Map<Blokkering>(m => m.Requires("Type").HasValue("BLOKERING"));
        }
    }
}