using Projecten2.NET.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

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
            if(aantal <= 0)
            {
                throw new Exception("Een correct aantal ingeven (groter dan nul), alstublieft");
            }
            if (materiaal != null && ControleerBeschikbaarheid(materiaal, beginDatum, aantal) &&
                beginDatum >= GeefCorrecteDatumTerug())
            {
                SteekInReservatie(materiaal, aantal, beginDatum);
            }
            else
                throw new Exception("Reservate kan nu niet worden aangemaakt");
        }

        public void BlokkeerMateriaal(Materiaal materiaal, int aantal, DateTime beginDatum, IGebruikerRepository gebruikerRepository)
        {
            if (aantal <= 0)
            {
                throw new Exception("Een correct aantal ingeven (groter dan nul), alstublieft");
            }
            if (materiaal != null && /*ControleerBeschikbaarheid(materiaal, beginDatum, aantal) && */
                beginDatum >= GeefCorrecteDatumTerug())
            {
                int materiaalAantal = materiaal.Aantal;
                foreach (Blokkering b in materiaal.Blokkeringen)
                {
                    if (b.BeginDate == beginDatum)
                    {
                        materiaalAantal -= b.Aantal;
                    }
                }
                if (materiaalAantal < aantal)
                {
                    throw new Exception("Er zijn nog maar " + materiaalAantal + " materialen over om te blokkeren");
                }

                foreach (Reservatie r in materiaal.Reservaties)
                {
                    if (r.BeginDate == beginDatum)
                    {
                        materiaalAantal -= r.Aantal;
                    }
                }

                if (materiaalAantal >= aantal)
                {
                    SteekInBlokkering(materiaal, aantal, beginDatum);
                }
                else
                {
                    while (materiaalAantal < aantal)
                    {
                        Reservatie res = materiaal.Reservaties.Where(r => r.BeginDate.Equals(beginDatum)).LastOrDefault();
                        if (res != null)
                        {
                            materiaalAantal += res.Aantal;
                            Gebruiker reservatieGebruiker = gebruikerRepository.FindById(res.GebruikerId);
                            mailStudentAfzeggingReservatie(reservatieGebruiker.Email, res.Materiaal.Artikelnaam, res.Aantal);
                     //       res.Gebruiker.Reservaties.Remove(res);
                            res.Materiaal.Reservaties.Remove(res);
                            reservatieGebruiker.Reservaties.Remove(res);
                        }
                        else
                        {
                            throw new Exception("Reservatie kan nu niet worden verwijderd");
                        }
                    }
                    SteekInBlokkering(materiaal, aantal, beginDatum);
                }
            }
            else
                throw new Exception("U bent te laat om voor desbetreffende week nog dit materiaal te blokkeren.");
        }

        public void RemoveReservatieFromReservaties(Reservatie r)
        {
            if (r != null)
            {
                r.Materiaal.Reservaties.Remove(r);
                Reservaties.Remove(r);
            }
            else
            {
                throw new Exception("Reservatie kan nu niet worden verwijderd");
            }
        }
        public void RemoveBlokkeringFromBlokkeringen(Blokkering b)
        {
            if (b != null)
            {
                b.Materiaal.Blokkeringen.Remove(b);
                Blokkeringen.Remove(b);
            }
            else
            {
                throw new Exception("Blokkering kan nu niet worden verwijderd");
            }
        }

        public Boolean BezitVerlanglijstMateriaal(Materiaal m)
        {
            return Verlanglijst.BezitVerlanglijstMateriaal(m);
        }

        public Reservatie FindReservatieByVoorbehoudingId(int voorbehoudingId)
        {
            return Reservaties.FirstOrDefault(r => r.EqualVoorbehoudingId(voorbehoudingId));
        }

        public Blokkering FindBlokkeringByVoorbehoudingId(int voorbehoudingId)
        {
            return Blokkeringen.FirstOrDefault(b => b.EqualVoorbehoudingId(voorbehoudingId));
        }

        private Boolean ControleerBeschikbaarheid(Materiaal materiaal, DateTime begindatum, int aantal)
        {
            var i = materiaal.Aantal;
            foreach (Blokkering blokkering in materiaal.Blokkeringen)
            {
                if (blokkering.BeginDate == begindatum)
                    i -= blokkering.Aantal;
            }
            foreach (Reservatie reservatie in materiaal.Reservaties)
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
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
            {
                beginDatum = GetNextWeekday(DateTime.Today.AddDays(7), DayOfWeek.Monday);
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

        private void SteekInReservatie(Materiaal materiaal, int aantal, DateTime beginDatum)
        {
            Boolean gereserveerd = false;
                foreach (Reservatie reservatie in Reservaties.Where(reservatie => reservatie.Materiaal.Artikelnaam == materiaal.Artikelnaam))
                {
                    if (reservatie.BeginDate == beginDatum)
                    {
                        reservatie.Aantal += aantal;
                        gereserveerd = true;
                        break;
                    }
                }
            if (!gereserveerd)
            {
                Reservatie r = new Reservatie(materiaal, beginDatum, aantal, GebruikerID);
                Reservaties.Add(r);
                materiaal.Reservaties.Add(r);
            }

        }

        private void SteekInBlokkering(Materiaal materiaal, int aantal, DateTime beginDatum)
        {
            Boolean gereserveerd = false;
                foreach (Blokkering blokkering in Blokkeringen.Where(blok => blok.Materiaal.Artikelnaam == materiaal.Artikelnaam))
                {
                    if (blokkering.BeginDate == beginDatum)
                    {
                        gereserveerd = true;
                        blokkering.Aantal += aantal;
                        break;
                    }
                }
            if (!gereserveerd)
            {
                Blokkering b = new Blokkering(materiaal, beginDatum, aantal, GebruikerID);
                Blokkeringen.Add(b);
                materiaal.Blokkeringen.Add(b);
            }
        }

        public int GetBeschikbaar(Materiaal materiaal, DateTime dateTime)
        {
            int beschikbaar = materiaal.Aantal;
            if(this.Type == Type.PERSONEEL)
            {
                foreach (Reservatie reservatie in materiaal.Reservaties)
                {
                    if (reservatie.BeginDate != null && dateTime == reservatie.BeginDate)
                        beschikbaar = beschikbaar - reservatie.Aantal;
                }
            }         

            foreach (Blokkering blok in materiaal.Blokkeringen)
            {
                if (blok.BeginDate != null && dateTime == blok.BeginDate)
                    beschikbaar = beschikbaar - blok.Aantal;
            }
            return beschikbaar;
        }

        public void mailStudentAfzeggingReservatie(string email, string artikelnaam, int aantal)
        {
            string myGmailAddress = "HoGent.DidactischeLeermiddelen@gmail.com";
            string appSpecificPassword = "Leermiddelen";

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.EnableSsl = true;
            client.Port = 587;
            client.Credentials = new NetworkCredential(myGmailAddress, appSpecificPassword);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(myGmailAddress);
            message.Sender = new MailAddress(myGmailAddress);
            message.To.Add(new MailAddress(email));
            message.Subject = "Annulatie reservatie van " + artikelnaam;
            message.Body = "Beste, Uw reservatie van " + aantal + " " + artikelnaam + " is geannuleerd. Het spijt ons voor het ongemak. Met vriendelijke Groeten, HoGent"; //Tijd over : beter uitwerken begin/uitdatum van reservatie.

            client.Send(message);
        }
    }
}