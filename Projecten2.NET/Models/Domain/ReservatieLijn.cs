using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Projecten2.NET
{
    public class ReservatieLijn
    {
        public int MateriaalId { get; set; }
        public int ReservatieId { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDat { get; private set; }
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "{0} moet positief zijn!")]
        public int Aantal { get; set; }
    }
}