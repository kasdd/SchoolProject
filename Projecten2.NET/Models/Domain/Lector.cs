using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Projecten2.NET
{
    public class Lector : Gebruiker
    {
        public override void NieuwWachtwoord(string wachtwoord)
        {
            this.Wachtwoord = wachtwoord;
        }

        //Als andere lector iets heeft gereserveerd --> niet in catalogus
        //Als student iets heeft uitgeleend --> wel in catalogus
        public override ICollection<Materiaal> GeefCorrecteCatalogus(Gebruiker gb)
        {
            ICollection<Materiaal> correcteCatalogus = null;
            foreach (Materiaal m in gb.Materialen)
            {
                if (m.Uitleenbaar)
                    correcteCatalogus.Add(m);
            }
            foreach (Reservatie r in Reservaties)
            {
                if(!r.Materiaal.Uitleenbaar && r.Gebruiker.GetType() == typeof (Student))
                    correcteCatalogus.Add(r.Materiaal);
            }
            return correcteCatalogus;
        }
    }
}