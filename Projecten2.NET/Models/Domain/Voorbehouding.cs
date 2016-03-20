using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2.NET.Models.Domain
{
    public abstract class Voorbehouding
    {
        public int VoorbehoudingId { get; set; }
        public Materiaal Materiaal { get; set; }
        public DateTime? BeginDate { get; protected set; }
        public DateTime? EndDate { get; set; }
        public int Aantal { get; set; }

        public Voorbehouding(Materiaal materiaal, DateTime begin, int aantal) : this()
        {
            this.BeginDate = begin;
            this.EndDate = StelEinddatumIn(begin);
            this.Materiaal = materiaal;
            this.Aantal = aantal;
        }

        public bool EqualVoorbehoudingId(int id)
        {
            return VoorbehoudingId.Equals(id);
        }

        public Voorbehouding()
        {
        
        }       

        protected DateTime? StelEinddatumIn(DateTime begin)
        {
            return EndDate = begin.AddDays(4);
        }
    }
}