using Projecten2.NET.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projecten2.NET
{
    public class Reservatie : Voorbehouding
    {

        public Reservatie(Materiaal materiaal, DateTime begin, int aantal, Gebruiker gebruiker) : base()
        {
            this.Gebruiker = gebruiker;
            this.BeginDate = begin;
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
                if (((obj as Reservatie).Materiaal == Materiaal) && ((obj as Reservatie).BeginDate.Equals(BeginDate)))
                    return true;
            return false;
        }
    }
}