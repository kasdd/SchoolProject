using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Projecten2.NET
{
    public class Materiaal
    {
        public int MateriaalId { get; private set; }
        public Boolean Uitleenbaar { get; set; }
        [DisplayName("Artikel nummer")]
        public String ArtikelNummer { get; set; }
        [DisplayName("Artikel")]
        public string Artikelnaam { get; set; }
        public string Omschrijving { get; set; }
        public double Prijs { get; set; }
        public string Foto { get; set; }
        public String Plaats { get; set; }
        public int Aantal { get; set; }
        public virtual ICollection<ReservatieLijn> Reservatielijnen { get; set; }
        public virtual ICollection<Doelgroep> Doelgroepen { get; set; }
        public virtual ICollection<Leergebied> Leergebieden { get; set; }

        public Materiaal()
        {
            Reservatielijnen = new List<ReservatieLijn>();
            Doelgroepen = new List<Doelgroep>();
            Leergebieden = new List<Leergebied>();
            Uitleenbaar = true;
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
    }
}