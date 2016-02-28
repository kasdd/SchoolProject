using System;
using System.Collections.Generic;

namespace Projecten2.NET
{
    public class Reservatie
    {
        public int ReservatieId { get; private set; }
        public virtual Gebruiker Gebruiker { get; set; }
        public virtual ICollection<Materiaal> Materialen { get; set; }
        public virtual ICollection<ReservatieLijn> ReservatieLijnen { get; set; }

        public Reservatie()
        {
            Materialen = new List<Materiaal>();
            ReservatieLijnen = new List<ReservatieLijn>();
        }
        public Reservatie(Gebruiker gebruiker, List<Materiaal> materialen, DateTime begin, DateTime eind)
        {
            
        }

        public bool Equals(object obj)
        {
            if (obj != null && obj is Reservatie)
                if ((obj as Reservatie).ReservatieId == ReservatieId)
                    return true;
            return false;
        }

        public int GetHashCode()
        {
            return ReservatieId;
        }
    }
}