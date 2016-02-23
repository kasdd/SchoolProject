using System;

namespace Projecten2.NET
{
    public class Student : Gebruiker
    {

        public String StudentenNr { get; set; }

        public Student()
        {

        }
        public Student(string Loginnaam, string Naam, string Voornaam, String Email, int GebruikerID, String Wachtwoord, String StudentenNr) : base (Loginnaam, Naam, Voornaam, Email, GebruikerID, Wachtwoord)
        {
            this.StudentenNr = StudentenNr;
        }

        public override void NieuwWachtwoord(string wachtwoord)
        {
            this.Wachtwoord = wachtwoord;
        }

        public override void reserveerMateriaal(Materiaal materiaal)
        {
            throw new NotImplementedException();
        }
    }
}