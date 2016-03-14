using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Projecten2.NET.Tests.Controllers
{
    public class DummyContext
    {

        private Gebruiker student;
        private Gebruiker lector;

        private Leergebied nederlands;
        private Leergebied aardrijkskunde;
        private Doelgroep kleuters;
        private Doelgroep lagerOnderwijs;

        private Materiaal wereldbol;
        private Materiaal dobbelsteen;

        private IList<Materiaal> materialen;

        public DummyContext()
        {
            materialen = new List<Materiaal>();
            //student = new Gebruiker { Name = "Gent", Postalcode = "9000" };
            nederlands = new Leergebied { LeergebiedNaam = "spelenderwijs", LeergebiedId = 1 };
            aardrijkskunde = new Leergebied { LeergebiedNaam = "aardrijkskunde", LeergebiedId = 2 };
            kleuters = new Doelgroep { DoelgroepNaam = "kleuters", DoelgroepId = 11 };
            lagerOnderwijs = new Doelgroep { DoelgroepNaam = "lagerOnderwijs", DoelgroepId = 12 };
            wereldbol = new Materiaal
            {
                Aantal = 3,
                Artikelnaam = "wereldbol",
                ArtikelNummer = "101",
                Doelgroepen = new List<Doelgroep> { lagerOnderwijs, kleuters },
                Foto = "fotostring",
                Leergebieden = new List<Leergebied> { aardrijkskunde },
                Omschrijving = "omschrijving van wereldbol",
                Plaats = "B.2.12",
                Prijs = 3,
                Uitleenbaar = true
            };
            dobbelsteen = new Materiaal
            {
                Aantal = 1,
                Artikelnaam = "dobbelsteen",
                ArtikelNummer = "102",
                Doelgroepen = new List<Doelgroep> { lagerOnderwijs },
                Foto = "fotostring dobbelsteen",
                Leergebieden = new List<Leergebied> { nederlands },
                Omschrijving = "omschrijving van dobbelsteen",
                Plaats = "B.1.39",
                Prijs = 195,
                Uitleenbaar = true
            };
            kleuters.addMateriaal(wereldbol);
            lagerOnderwijs.addMateriaal(wereldbol);
            lagerOnderwijs.addMateriaal(dobbelsteen);
            materialen.Add(wereldbol);
            materialen.Add(dobbelsteen);


        }
        public IQueryable<Materiaal> AllMaterialen
        {
            get
            {
                return materialen.AsQueryable();
            }
        }

        public IQueryable<Doelgroep> AllDoelgroepen
        {
            get
            {
                return new List<Doelgroep>
                       {
                           kleuters,
                           lagerOnderwijs
                       }.AsQueryable();
            }
        }

        public Materiaal FindByArtikelNaamMaterialen(String naam)
        {
            return materialen.FirstOrDefault(m => m.Artikelnaam == naam);
        }

        public Doelgroep FindByIdDoelgroep(int nummer)
        {
            if (nummer == 11)
                return kleuters;
            if (nummer == 12)
                return lagerOnderwijs;
            else
                return null;
        }
    }
}
