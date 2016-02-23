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
        public Beheerder()
        {

        }
        public Beheerder(string Loginnaam, string Naam, string Voornaam, String Email, int GebruikerID, String Wachtwoord) : base (Loginnaam, Naam, Voornaam, Email, GebruikerID, Wachtwoord)
        {

        }

        public override void NieuwWachtwoord(string wachtwoord)
        {
            throw new NotImplementedException();
        }

        public override void reserveerMateriaal(Materiaal materiaal)
        {
            throw new NotImplementedException();
        }
    }
}