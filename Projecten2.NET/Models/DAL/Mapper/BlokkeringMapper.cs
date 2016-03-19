using Projecten2.NET.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Projecten2.NET.Models.DAL.Mapper
{
    public class BlokkeringMapper : EntityTypeConfiguration<Blokkering>
    {
        public BlokkeringMapper()
        {
            //ToTable("Blokkering");

            

            Map(m =>
            {
                m
              .MapInheritedProperties();
                m.ToTable("Blokkering");
            });

            HasKey(m => m.VoorbehoudingId);

        }
    }
}