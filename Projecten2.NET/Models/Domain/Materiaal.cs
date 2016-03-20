using Projecten2.NET.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Projecten2.NET
{
    public class Materiaal
    {
        public int MateriaalId { get; private set; }
        public Boolean Uitleenbaar { get; set; }
        public String ArtikelNummer { get; set; }
        public string Artikelnaam { get; set; }
        public string Omschrijving { get; set; }
        public string Firma { get; set; }
        public double Prijs { get; set; }
        public string Foto { get; set; }
        public String Plaats { get; set; }    
        public int Aantal { get; set; }
        public virtual ICollection<Reservatie> Reservaties { get; set; }
        public virtual ICollection<Blokkering> Blokkeringen { get; set; }
        public virtual ICollection<Doelgroep> Doelgroepen { get; set; }
        public virtual ICollection<Leergebied> Leergebieden { get; set; }

        public Materiaal()
        {
            Blokkeringen = new List<Blokkering>();
            Reservaties = new List<Reservatie>();
            Doelgroepen = new List<Doelgroep>();
            Leergebieden = new List<Leergebied>();
        }

        public bool Equals(object obj)
        {
            if (obj != null && obj is Materiaal)
                if ((obj as Materiaal).MateriaalId == MateriaalId)
                    return true;
            return false;
        }

        public int GetHashCode()
        {
            return MateriaalId;
        }

        public bool BevatDoelgroep(string naam)
        {
            return Doelgroepen.Any(doelgroep => doelgroep.DoelgroepNaam.ToLower().Contains(naam.ToLower()));
        }

    }
}