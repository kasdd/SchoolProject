namespace Projecten2.NET.Models
{
    public class Lijst
    {

        public string LijstId { get; set; }
        public int MateriaalId { get; set; }
        public int Aantal { get; set; }
        public System.DateTime Aangemaakt { get; set; }
        public virtual Materiaal Materiaal { get; set; }


    }
}