using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Common.EntitySql;
using System.Web.Mvc;

namespace Projecten2.NET.Models.ViewModels
{
    public class ReservatieViewModel : VoorbehoudingViewModel
    {
        public ReservatieViewModel (Materiaal materiaal): base()
        {
            this.Materiaal = materiaal;
            this.beginDatum = GeefCorrecteDatumTerug();
            this.aantal = materiaal.Aantal;
            this.Beschikbaar = AantalBeschikbaar(beginDatum);
        }

        public ReservatieViewModel() : base()
        {

        }
    }

    public class BlokkeringViewModel : VoorbehoudingViewModel
    {
        public BlokkeringViewModel(Materiaal materiaal) : base()
        {
            this.Materiaal = materiaal;
            this.beginDatum = GeefCorrecteDatumTerug();
            this.aantal = materiaal.Aantal;
            this.Beschikbaar = AantalBeschikbaar(beginDatum);
        }

        public BlokkeringViewModel() : base()
        {

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
        public int Beschikbaar { get; set; }

        public VoorbehoudingViewModel(Materiaal materiaal)
        {
            this.Materiaal = materiaal;
            this.beginDatum = GeefCorrecteDatumTerug();
            this.aantal = materiaal.Aantal;
            this.Beschikbaar = AantalBeschikbaar(beginDatum);
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

        public int AantalBeschikbaar(DateTime datum)
        {

            Beschikbaar = aantal;
            foreach (Reservatie lijn in Materiaal.Reservatielijnen)
            {
                if (lijn.BeginDate != null && datum == lijn.BeginDate.Value)
                {
                    Beschikbaar--;
                }
            }
            return Beschikbaar;
        }

    }
}
