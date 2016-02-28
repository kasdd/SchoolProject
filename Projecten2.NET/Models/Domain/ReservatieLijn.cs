using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Projecten2.NET
{
    public class ReservatieLijn
    {
        public int MateriaalId { get; set; }
        public int ReservatieId { get; set; }
        public virtual Materiaal Materiaal { get; set; }
        public DateTime BeginDat { get; private set; }
        public DateTime EindDat { get; private set; }

    }
}