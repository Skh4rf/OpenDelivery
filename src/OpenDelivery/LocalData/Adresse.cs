using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDelivery.LocalData
{
    internal class Adresse
    {
        public int Adressnummer { get; set; }
        public int Postleitzahl { get; set; }
        public string Strasse { get; set; }
        public int Nummer { get; set; }
        public string Adresszusatz { get; set; }
        public Koordinate koordinate;

        public Adresse() { }
    }
}
