﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Projecten2.NET.Tests.Controllers
{
    public class DummyContext
    {

        public Gebruiker student1;
        public Gebruiker student2;
        public Gebruiker student3;
        public Gebruiker personeel1;
        public Gebruiker personeel2;

        public Leergebied nederlands;
        public Leergebied aardrijkskunde;
        public Doelgroep kleuters;
        public Doelgroep lagerOnderwijs;

        public Materiaal wereldbol;
        public Materiaal dobbelsteen;

        public IList<Materiaal> materialen;

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
                Aantal = 13,
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
                Aantal = 10,
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

            student1 = new Gebruiker()
            {
                Email = "student1@student.hogent.be",
                Naam = "Student1",
                Voornaam = "Student1",
                Type = Type.STUDENT
            };
            student2 = new Gebruiker()
            {
                Email = "student2@student.hogent.be",
                Naam = "Student2",
                Voornaam = "Student2",
                Type = Type.STUDENT
            };
            student3 = new Gebruiker()
            {
                Email = "student2@student.hogent.be",
                Naam = "Student2",
                Voornaam = "Student2",
                Type = Type.STUDENT
            };

            personeel1 = new Gebruiker()
            {
                Email = "personeel1@personeel.hogent.be",
                Naam = "Personeel1",
                Voornaam = "Personeel1",
                Type = Type.PERSONEEL
            };
            personeel2 = new Gebruiker()
            {
                Email = "personeel2@personeel.hogent.be",
                Naam = "personeel2",
                Voornaam = "personeel2",
                Type = Type.PERSONEEL
            };

            student1.AddMateriaalToVerlanglijst(wereldbol);

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
