using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2.NET.Models.ViewModels
{
    public class ReservatieLijnViewModel
    {
        public int MateriaalId { get; set; }
        public int ReservatieId { get; set; }
        public DateTime BeginDat { get; private set; }
        public int Aantal { get; set; }
    }
}