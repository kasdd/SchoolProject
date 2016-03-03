using System;
using System.Collections.Generic;
using System.Linq;

namespace Projecten2.NET
{
    public class Reservatie
    {
        public int ReservatieId { get; private set; }
        public virtual Gebruiker Gebruiker { get; set; }
        public virtual ICollection<Materiaal> Materialen { get; set; }
        public virtual List<ReservatieLijn> ReservatieLijnen { get; set; }
        public IEnumerable<ReservatieLijn> ReservatieLijnenAsEnumerable { get { return ReservatieLijnen.AsEnumerable(); } }

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

        public void AddReservatieLijn(Materiaal materiaal, int quantity)
        {
            ReservatieLijn reservatielijn = ReservatieLijnen.SingleOrDefault(r => r.MateriaalId.Equals(materiaal.MateriaalId));
            if (reservatielijn == null)
                ReservatieLijnen.Add(new ReservatieLijn { MateriaalId = materiaal.MateriaalId, Aantal = quantity });
            else reservatielijn.Aantal += quantity;
        }

        public void RemoveReservatieLijn(Materiaal materiaal)
        {
            ReservatieLijn reservatielijn = GetReservatieLijn(materiaal.MateriaalId);
            if (reservatielijn != null)
                ReservatieLijnen.Remove(reservatielijn);
        }

        public ReservatieLijn GetReservatieLijn(int materiaalID)
        {
            return ReservatieLijnen.SingleOrDefault(r => r.MateriaalId == materiaalID);
        }
    }
}