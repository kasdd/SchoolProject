﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projecten2.NET.Models.ViewModels
{

    public class VerlanglijstViewModel
    {
        public int MateriaalId { get; private set; }
        public Boolean Uitleenbaar { get; set; }
        [DisplayName("Artikel nummer")]
        public String ArtikelNummer { get; set; }
        [DisplayName("Artikel")]
        public string Artikelnaam { get; set; }
        public string Omschrijving { get; set; }
        public double Prijs { get; set; }
        public string Foto { get; set; }
        public String Plaats { get; set; }
        [DisplayName("Beschikbaar")]
        public int Aantal { get; set; }

        public VerlanglijstViewModel()
        {

        }

        public VerlanglijstViewModel(Materiaal m)
        {
            MateriaalId = m.MateriaalId;
            Artikelnaam = m.Artikelnaam;
            ArtikelNummer = m.ArtikelNummer;
            Omschrijving = m.Omschrijving;
            Prijs = m.Prijs;
            Foto = m.Foto;
            Plaats = m.Plaats;
            Aantal = m.Aantal;
            Uitleenbaar = m.Uitleenbaar;

        }

    }
}