﻿using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace Projecten2.NET
{
    public class Student : Gebruiker
    {

        public String StudentenNr
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public override void NieuwWachtwoord(string wachtwoord)
        {
            this.Wachtwoord = wachtwoord;
        }
    }
}