using System;
using System.Collections.Generic;
using System.Linq;

namespace Projecten2.NET
{
    public class Reservatie
    {
        public int ReservatieId { get; set; }
        public Materiaal Materiaal { get; set; }
        public DateTime? BeginDat { get; private set; }
        public DateTime? EndDate { get; set; }
        public int Aantal { get; set; }

        public Reservatie(Materiaal materiaal, DateTime begin, int aantal) :this()
        {
            this.BeginDat = begin;
            this.EndDate = StelEinddatumIn(begin);
            this.Materiaal = materiaal;
            this.Aantal = aantal;
        }

        private Reservatie()
        {
            
        }

        public bool Equals(object obj)
        {
            if (obj != null && obj is Reservatie)
                if ((obj as Reservatie).ReservatieId == ReservatieId)
                    return true;
            return false;
        }

        public int GetHashCode()
        {
            return ReservatieId;
        }

        private DateTime? StelEinddatumIn(DateTime begin)
        {
            return EndDate = begin.AddDays(5);
        }
    }
}