using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Projecten2.NET
{
    public class Beheerder : Gebruiker
    {
        public int ID { get; set; }

        public String Naam { get; set; }

        public override void NieuwWachtwoord(string wachtwoord)
        {
            throw new NotImplementedException();
        }

        public override ICollection<Materiaal> GeefCorrecteCatalogus(Gebruiker gb)
        {
            ICollection<Materiaal> correcteCatalogus = null;

            foreach (Materiaal m in gb.Materialen)
            {
                    correcteCatalogus.Add(m);
            }
            return correcteCatalogus;
        }

        public override void reserveerMateriaal(Materiaal materiaal)
        {
            throw new NotImplementedException();
        }
    }
}