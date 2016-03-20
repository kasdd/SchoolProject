using System.ComponentModel.DataAnnotations;
using Projecten2.NET.Models.Domain;
using System;

namespace Projecten2.NET.Models.ViewModels
{
    public class ReservatieViewModel : VoorbehoudingViewModel
    {
        public ReservatieViewModel(Materiaal materiaal, Gebruiker gebruiker, Reservatie r) : base()
        {
            this.Materiaal = materiaal;
            this.beginDatum = GeefCorrecteDatumTerug();
            this.aantal = materiaal.Aantal;
            this.Beschikbaar = GetBeschikbaar(materiaal, beginDatum, gebruiker);
            this.beginDatum = r.BeginDate;
            this.eindDatum = r.EndDate;
            this.reservatie = r;
        }

        public ReservatieViewModel(Materiaal materiaal, Gebruiker gebruiker) : base()
        {
            this.Materiaal = materiaal;
            this.beginDatum = GeefCorrecteDatumTerug();
            this.aantal = materiaal.Aantal;
            this.Beschikbaar = GetBeschikbaar(materiaal, beginDatum, gebruiker);
        }
        public ReservatieViewModel() : base()
        {

        }

        public int GetBeschikbaar(Materiaal materiaal, DateTime dateTime, Gebruiker gebruiker)
        {
            int beschikbaar = materiaal.Aantal;
            if (gebruiker.Type == Type.PERSONEEL)
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
    }

    public class BlokkeringViewModel : VoorbehoudingViewModel
    {
        public BlokkeringViewModel(Materiaal materiaal, Gebruiker gebruiker, Blokkering b) : base()
        {
            this.Materiaal = materiaal;
            this.beginDatum = GeefCorrecteDatumTerug();
            this.aantal = materiaal.Aantal;
            this.Beschikbaar = GetBeschikbaar(materiaal, beginDatum, gebruiker);
            this.beginDatum = b.BeginDate;
            this.eindDatum = b.EndDate;
            this.blokkering = b;
        }
        public BlokkeringViewModel(Materiaal materiaal, Gebruiker gebruiker) : base()
        {
            this.Materiaal = materiaal;
            this.beginDatum = GeefCorrecteDatumTerug();
            this.aantal = materiaal.Aantal;
            this.Beschikbaar = GetBeschikbaar(materiaal, beginDatum, gebruiker);
        }

        public BlokkeringViewModel() : base()
        {

        }
        public int GetBeschikbaar(Materiaal materiaal, DateTime dateTime, Gebruiker gebruiker)
        {
            int beschikbaar = materiaal.Aantal;
            if (gebruiker.Type == Type.PERSONEEL)
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
    }

    public class VoorbehoudingViewModel
    {

        public Materiaal Materiaal { get; set; }
        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Aantal")]
        [Range(1, int.MaxValue, ErrorMessage = "U moet minimum 1 materiaal aanklikken")]
        public int aantal { get; set; }
        [Required(ErrorMessage = "Gelieve uw startdatum in te voeren")]
        [DataType(DataType.Date)]
        [Display(Name = "Startdatum")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime beginDatum { get; set; }
        [Required(ErrorMessage = "Gelieve uw startdatum in te voeren")]
        [DataType(DataType.Date)]
        [Display(Name = "Startdatum")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime eindDatum { get; set; }
        public int Beschikbaar { get; set; }
        public Blokkering blokkering { get; set; }
        public Reservatie reservatie { get; set; }

        public VoorbehoudingViewModel(Materiaal materiaal)
        {
            this.Materiaal = materiaal;
            this.beginDatum = GeefCorrecteDatumTerug();
            this.aantal = materiaal.Aantal;
        }

        public VoorbehoudingViewModel()
        {

        }

        public DateTime GeefCorrecteDatumTerug()
        {
            beginDatum = new DateTime();
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

        public static DateTime GetNextWeekday(DateTime vandaag, DayOfWeek verwachteDag)
        {
            int daysToAdd = ((int)verwachteDag - (int)vandaag.DayOfWeek + 7) % 7;
            return vandaag.AddDays(daysToAdd);
        }
    }
}