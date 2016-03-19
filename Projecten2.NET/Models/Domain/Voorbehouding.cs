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

        public Voorbehouding()
        {

        }

        public bool Equals(object obj)
        {
            if (obj != null && obj is Blokkering)
                if ((obj as Blokkering).VoorbehoudingId == VoorbehoudingId)
                    return true;
            return false;
        }
        protected DateTime? StelEinddatumIn(DateTime begin)
        {
            return EndDate = begin.AddDays(4);
        }

        public int GetHashCode()
        {
            return VoorbehoudingId;
        }
    }
}