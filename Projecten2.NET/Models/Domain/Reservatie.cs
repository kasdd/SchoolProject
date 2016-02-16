using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Projecten2.NET
{
    public class Reservatie
    {
        public int ReservatieId { get; set; }
        public DateTime BeginDat { get; set; }
        public DateTime EindDat { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public Materiaal Materiaal { get; set; }
    }
}