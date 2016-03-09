using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projecten2.NET
{
    public class Doelgroep
    {
        [Key]
        public int DoelgroepId { get; set; }

        public String DoelgroepNaam { get; set; }

        public virtual ICollection<Materiaal> Materialen { get; set; }

        public Doelgroep()
        {
            Materialen = new List<Materiaal>();
        }

        public void addMateriaal(Materiaal materiaal)
        {
            Materialen.Add(materiaal);
        }

    }
}