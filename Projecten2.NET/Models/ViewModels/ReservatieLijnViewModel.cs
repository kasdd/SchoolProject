using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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
        public DateTime BeginDatum { get; set; }

        public NieuweReservatieViewModel(Materiaal materiaal)
        {
            this.BeginDatum = DateTime.Today;
            this.materiaal = materiaal;
        }

    }
}
