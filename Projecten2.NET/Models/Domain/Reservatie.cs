using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace Projecten2.NET
{
    public class Reservatie
    {
        public string ReservatieId { get; set; }
        public DateTime BeginDat { get; set; }
        public DateTime EindDat { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public List<Materiaal> Materialen { get; set; }
    }
}