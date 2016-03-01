using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace Projecten2.NET
{
    public class Verlanglijst
    { 
        public int VerlanglijstId { get; set; }
        //public Gebruiker gebruiker { get; set; }
        public virtual ICollection<Materiaal> Materialen { get; set; }
        public Verlanglijst()
        {
           Materialen = new List<Materiaal>();
        }
    }
}