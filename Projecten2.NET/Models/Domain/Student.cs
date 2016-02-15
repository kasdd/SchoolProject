using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Linq;

namespace Projecten2.NET
{
    public class Student : Gebruiker
    {

        public String StudentenNr { get; set; }

        public override void NieuwWachtwoord(string wachtwoord)
        {
            this.Wachtwoord = wachtwoord;
        }

        public override ICollection<Materiaal> GeefCorrecteCatalogus(Gebruiker gb)
        {
            ICollection<Materiaal> correcteCatalogus = null;

            foreach (Materiaal m in gb.Materialen)
            {
                if(m.Uitleenbaar)
                    correcteCatalogus.Add(m);
            }
            return correcteCatalogus;
        }
    }
}