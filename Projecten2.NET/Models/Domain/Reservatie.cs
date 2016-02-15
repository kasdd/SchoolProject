using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Projecten2.NET
{
    public class Reservatie
    {
        public int numberOfItems
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Gebruiker Gebruiker
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
        public Materiaal Materiaal { get; set; }
    }
}