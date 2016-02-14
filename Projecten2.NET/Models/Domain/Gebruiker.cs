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
        protected Gebruiker()
        {
            throw new System.NotImplementedException();
        }

        protected Gebruiker(string Loginnaam, string Naam, string Voornaam)
        {
            throw new System.NotImplementedException();
        }

        public string Naam
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public String Email
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public int GebruikerID
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public String Loginnaam
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string Voornaam
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public string Wachtwoord
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public abstract void NieuwWachtwoord(string wachtwoord);

        public void ZendEmail(string emailadres, string onderwerp, string bericht)
        {
            throw new System.NotImplementedException();
        }

        public Materiaal Materiaal
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}