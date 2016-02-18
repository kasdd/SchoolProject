using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Projecten2.NET
{
    public abstract class Gebruiker
    {

        public string Naam { get; set; }

        public String Email { get; set; }

        public int GebruikerID { get; set; }


        public String Loginnaam { get; set; }

        public string Voornaam { get; set; }
         
        public string Wachtwoord { get; set; }

        public abstract void NieuwWachtwoord(string wachtwoord);

        public void ZendEmail(string emailadres, string onderwerp, string bericht)
        {
            throw new System.NotImplementedException();
        }

        public virtual ICollection<Materiaal> Materialen { get; set; }
        public virtual ICollection<Reservatie> Reservaties { get; set; }

        public Gebruiker()
        {
            Materialen = new List<Materiaal>();
            Reservaties = new List<Reservatie>();
        }

        protected Gebruiker(string Loginnaam, string Naam, string Voornaam)
        {
            throw new System.NotImplementedException();
        }

        protected Gebruiker(string Loginnaam, string Naam, string Voornaam, String Email, int GebruikerID, String Wachtwoord)
        {
            this.Loginnaam = Loginnaam;
            this.Naam = Naam;
            this.Voornaam = Voornaam;
            this.Email = Email;
            this.GebruikerID = GebruikerID;
            this.Wachtwoord = Wachtwoord;
        }

        public abstract ICollection<Materiaal> GeefCorrecteCatalogus(Gebruiker gb);

        public abstract void reserveerMateriaal(Materiaal materiaal);
    }
}