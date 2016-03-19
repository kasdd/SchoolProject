using Projecten2.NET.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public virtual ICollection<Blokkering> Blokkeringen { get; set; }
        public virtual Verlanglijst Verlanglijst { get; set; }
        public Gebruiker()
        {
            Blokkeringen = new List<Blokkering>();
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
            if (m != null)
                Verlanglijst.RemoveMateriaal(m);
            else
            {
                throw new Exception("Materiaal kan nu niet van lijst worden verwijderd");
            }
        }

        public void ReserveerMateriaal(Materiaal materiaal, int aantal, DateTime beginDatum)
        {
            if (materiaal != null && ControleerBeschikbaarheid(materiaal, beginDatum, aantal) &&
                beginDatum > GeefCorrecteDatumTerug())
            {
                if (!BezitReedsReservatie(materiaal, aantal))
                {
                    Reservatie r = new Reservatie(materiaal, beginDatum, aantal);
                    Reservaties.Add(r);
                    materiaal.Reservatielijnen.Add(r);
                }
            }
            else
                throw new Exception("Reservate kan nu niet worden aangemaakt");
        }

        public void BlokkeerMateriaal(Materiaal materiaal, int aantal, DateTime beginDatum)
        {
            if (materiaal != null && ControleerBeschikbaarheid(materiaal, beginDatum, aantal) /*&&
                beginDatum > GeefCorrecteDatumTerug()*/)
            {
                if (!BezitReedsReservatie(materiaal, aantal))
                {
                    Blokkering b = new Blokkering(materiaal, beginDatum, aantal);
                    Blokkeringen.Add(b);
                    materiaal.Blokkeringen.Add(b);
                }
            }
            else
                throw new Exception("Blokkering kan nu niet worden aangemaakt");
        }

        public void RemoveReservatieFromReservaties(Reservatie r)
        {
            if (r != null)
                Reservaties.Remove(r);
            else
            {
                throw new Exception("Reservatie kan nu niet worden verwijderd");
            }
        }
        public void RemoveBlokkeringFromBlokkeringen(Blokkering b)
        {
            if (b != null)
                Blokkeringen.Remove(b);
            else
            {
                throw new Exception("Blokkeringen kan nu niet worden verwijderd");
            }
        }

        public Boolean BezitVerlanglijstMateriaal(Materiaal m)
        {
            return Verlanglijst.BezitVerlanglijstMateriaal(m);
        }

        public Reservatie FindVoorbehoudingByVoorbehoudingId(int voorbehoudingId)
        {
            return Reservaties.FirstOrDefault(r => r.EqualVoorbehoudingId(voorbehoudingId));
        }

        private Boolean ControleerBeschikbaarheid(Materiaal materiaal, DateTime begindatum, int aantal)
        {
            var i = materiaal.Aantal;
            foreach (Blokkering blokkering in materiaal.Blokkeringen)
            {
                if (blokkering.BeginDate == begindatum)
                    i -= blokkering.Aantal;
            }
            foreach (Reservatie reservatie in materiaal.Reservatielijnen)
            {
                if (reservatie.BeginDate == begindatum)
                    i -= reservatie.Aantal;
            }
            return aantal <= i;
        }

        private DateTime GeefCorrecteDatumTerug()
        {
            DateTime beginDatum = new DateTime();
            if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
            {
                if (DateTime.Now.TimeOfDay.Hours < 17 /*Convert.ToDateTime("05:00:00 PM")*/)
                {
                    beginDatum = GetNextWeekday(DateTime.Today, DayOfWeek.Monday);
                }
                else
                {
                    beginDatum = GetNextWeekday(DateTime.Today.AddDays(7), DayOfWeek.Monday);
                }
            }
            else if (DateTime.Now.DayOfWeek < DayOfWeek.Friday)
            {
                beginDatum = GetNextWeekday(DateTime.Today.AddDays(1), DayOfWeek.Monday);
            }
            else
            {
                beginDatum = GetNextWeekday(DateTime.Today.AddDays(7), DayOfWeek.Monday);
            }

            return beginDatum;
        }

        private static DateTime GetNextWeekday(DateTime vandaag, DayOfWeek verwachteDag)
        {
            int daysToAdd = ((int)verwachteDag - (int)vandaag.DayOfWeek + 7) % 7;
            return vandaag.AddDays(daysToAdd);
        }

        private Boolean BezitReedsReservatie(Materiaal materiaal, int aantal)
        {
            foreach (Reservatie reservatie in Reservaties.Where(reservatie => reservatie.Materiaal.Artikelnaam == materiaal.Artikelnaam))
            {
                reservatie.Aantal += aantal;
                foreach (Reservatie r in materiaal.Reservatielijnen.Where(r => r.Materiaal.Artikelnaam == reservatie.Materiaal.Artikelnaam))
                    r.Aantal += aantal;
                return true;
            }
            return false;
        }

        public int GetBeschikbaar(Materiaal materiaal, DateTime dateTime)
        {
            int beschikbaar = materiaal.Aantal;
            foreach (Reservatie reservatie in materiaal.Reservatielijnen)
            {
                if (reservatie.BeginDate != null && dateTime == reservatie.BeginDate)
                    beschikbaar--;
            }
            return beschikbaar;
        }
    }
}