using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2.NET.Models.ViewModels
{
    public class VerlanglijstjeRemoveViewModel
    { 
        public double LijstTotaal { get; set; }
        public int LijstAantal { get; set; }
        public int ItemAantal { get; set; }
    public string Bericht { get; set; }
    public int DeleteId { get; set; }
    }
}