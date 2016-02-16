using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Projecten2.NET
{
    public class Materiaal
    {
        public Boolean Uitleenbaar { get; set; }

        public String ArtikelNummer { get; set; }

        public string Artikelnaam { get; set; }

        public string Omschrijving { get; set; }

        public String Doelgroep { get; set; }

        public double Prijs { get; set; }

        public int Leergebied { get; set; }

        public Reservatie Reservatie { get; set; }

        public string Foto { get; set; }

    }
}