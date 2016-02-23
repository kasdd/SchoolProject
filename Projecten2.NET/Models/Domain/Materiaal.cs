﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Projecten2.NET
{
    public class Materiaal
    {
        public int MateriaalId { get; private set; }
        public Boolean Uitleenbaar { get; set; }

        public String ArtikelNummer { get; set; }

        public string Artikelnaam { get; set; }

        public string Omschrijving { get; set; }

        public String Doelgroep { get; set; }

        public double Prijs { get; set; }

        public int Leergebied { get; set; }

        public ICollection<Reservatie> Reservaties { get; set; }

        public string Foto { get; set; }

        public Materiaal()
        {
            Uitleenbaar = false;
        }

        public bool Equals(object obj)
        {
            if (obj != null && obj is Materiaal)
                if ((obj as Materiaal).MateriaalId == MateriaalId)
                    return true;
            return false;
        }

        public int GetHashCode()
        {
            return MateriaalId;
        }
    }
}