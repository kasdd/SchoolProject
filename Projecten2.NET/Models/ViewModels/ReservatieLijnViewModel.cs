using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Common.EntitySql;

namespace Projecten2.NET.Models.ViewModels
{
    public class ReservatieLijnViewModel
    {
        public int MateriaalId { get; set; }
        public int ReservatieId { get; set; }
        public DateTime BeginDat { get; private set; }
        public int Aantal { get; set; }
    }

    public class NieuweReservatieViewModel
    {
        
        public Materiaal materiaal { get; set; }
        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Aantal")]
        [Range(1, int.MaxValue, ErrorMessage = "U moet minimim 1 materiaal aanklikken")]
        public int aantal { get; set; }
        [Required(ErrorMessage = "Gelieve uw startdatum in te voeren")]
        [DataType(DataType.Date)]
        [Display(Name = "Startdatum van reservatie")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime beginDatum { get; set; }

        public int beschikbaar { get; set; }

        public NieuweReservatieViewModel(Materiaal materiaal)
        {
            this.beginDatum = GeefCorrecteDatumTerug();
            this.materiaal = materiaal;
        }

        public NieuweReservatieViewModel() : this(new Materiaal())
        {
            
        }

        public DateTime GeefCorrecteDatumTerug()
        {

            beginDatum = DateTime.Today;
            return beginDatum;
        }

        public int AantalBeschikbaar(DateTime datum)
        {
            beschikbaar = aantal;
            foreach (ReservatieLijn lijn in materiaal.Reservatielijnen)
            {
                if (lijn.BeginDat != null && beginDatum == lijn.BeginDat.Value)
                {
                    beschikbaar--;
                }
            }
            return beschikbaar;
        }

    //    DateTime startdatum = new DateTime();
    //        if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
    //        {
    //            if (DateTime.Now.TimeOfDay.Hours< 17 /*Convert.ToDateTime("05:00:00 PM")*/)
    //            {
    //                startdatum = GetNextWeekday(DateTime.Today, DayOfWeek.Monday);
    //}
    //            else
    //            {
    //                startdatum = GetNextWeekday(DateTime.Today.AddDays(7), DayOfWeek.Monday);
    //            }
    //        }
    //        else if (DateTime.Now.DayOfWeek<DayOfWeek.Friday)
    //        {
    //            startdatum = GetNextWeekday(DateTime.Today.AddDays(1), DayOfWeek.Monday);
    //        }
    //        else
    //        {
    //            startdatum = GetNextWeekday(DateTime.Today.AddDays(7), DayOfWeek.Monday);
    //        }

    }
}
