using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Projecten2.NET.Models.ViewModels
{
    public class CatalogusViewModel
    {
        [DisplayName("Artikel")]
        public string Artikelnaam { get; set; }
        public string Omschrijving { get; set; }
        public double Prijs { get; set; }
        public string Foto { get; set; }
        public String Plaats { get; set; }
        public string Firma { get; set; }
        public bool InVerlanglijst { get; set; }
        public int Aantal { get; set; }
        public CatalogusViewModel(Gebruiker gebruiker, Materiaal materiaal)
        {
            this.Artikelnaam = materiaal.Artikelnaam;
            this.Omschrijving = materiaal.Omschrijving;
            this.Plaats = materiaal.Plaats;
            this.Prijs = materiaal.Prijs;
            this.Foto = materiaal.Foto;
            this.Aantal = materiaal.Aantal;
            this.Firma = materiaal.Firma;
            InVerlanglijst = ZitInVerlanglijst(gebruiker);
        }

        public CatalogusViewModel()
        {
            
        }
        private Boolean ZitInVerlanglijst(Gebruiker gebruiker)
        {
                foreach (Materiaal materiaal in gebruiker.Verlanglijst.Materialen)
                {
                    if (materiaal.Artikelnaam == Artikelnaam)
                        InVerlanglijst = true;
            }
                return InVerlanglijst;
        }
    }
}