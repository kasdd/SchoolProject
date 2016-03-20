using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2.NET.Models.Domain
{
    public class Blokkering : Voorbehouding
    {
        public Blokkering(Materiaal materiaal, DateTime begin, int aantal, int gebruikerId) : base()
        {
            this.BeginDate = begin;
            this.EndDate = StelEinddatumIn(begin);
            this.Materiaal = materiaal;
            this.Aantal = aantal;
            this.GebruikerId = gebruikerId;
            this.VoorbehoudingId = VoorbehoudingId;
        }

        private Blokkering()
        {

        }

        public bool Equals(object obj)
        {
            if (obj != null && obj is Blokkering)
                if (((obj as Blokkering).Materiaal == Materiaal) && ((obj as Blokkering).BeginDate.Equals(BeginDate)))
                    return true;
            return false;
        }
    }
}