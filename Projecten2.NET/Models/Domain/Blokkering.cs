using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2.NET.Models.Domain
{
    public class Blokkering : Voorbehouding
    {

        public int BlokkeringId { get; set; }

        public Blokkering(Materiaal materiaal, DateTime begin, int aantal) : base()
        {
            this.BeginDate = begin;
            this.EndDate = StelEinddatumIn(begin);
            this.Materiaal = materiaal;
            this.Aantal = aantal;
        }

        private Blokkering()
        {

        }
    }
}