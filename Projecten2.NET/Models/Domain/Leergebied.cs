using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projecten2.NET
{
    public class Leergebied
    {
        public String LeergebiedNaam { get; set; }
        [Key]
        public int LeergebiedId { get; set; }
        public virtual ICollection<Materiaal> Materialen { get; set; }

        public Leergebied()
        {
            Materialen = new List<Materiaal>();
        }
    }
}