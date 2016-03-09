using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Projecten2.NET
{
    public class Gebruiker
    {
        public String Faculteit { get; set; }
        public String Foto { get; set; }
        public string Naam { get; set; }
        public String Email { get; set; }
        public int GebruikerID { get; set; }
        public string Voornaam { get; set; }
        public Type Type { get; set; }
        public virtual ICollection<Reservatie> Reservaties { get; set; }
        public virtual Verlanglijst Verlanglijst { get; set; }
        public Gebruiker()
        {
            Reservaties = new List<Reservatie>();
            Verlanglijst = new Verlanglijst();
        }

        public void AddMateriaalToVerlanglijst(Materiaal m)
        {
            if (m != null)
                Verlanglijst.AddMateriaal(m);
            else
                throw new Exception("Materiaal kan nu niet worden toegevoegd");
        }

        public void RemoveMateriaalFromVerlanglijst(Materiaal m)
        {
            if(m!=null)
            Verlanglijst.RemoveMateriaal(m);
            else
            {
                throw new Exception("Materiaal kan nu niet van lijst worden verwijderd");
            }
        }

        public Boolean BezitVerlanglijstMateriaal(Materiaal m)
        {
            return Verlanglijst.BezitVerlanglijstMateriaal(m);
        }
    }
}