using System;
using System.Collections.Generic;

namespace Projecten2.NET
{
    public class Reservatie
    {
        public int ReservatieId { get; private set; }
        public DateTime BeginDat { get; private set; }
        public DateTime EindDat { get; private set; }
        public Gebruiker Gebruiker { get; set; }
        public ICollection<Materiaal> Materialen { get; set; }

        public Reservatie()
        {
            Materialen = new List<Materiaal>();
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