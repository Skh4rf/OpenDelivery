using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDelivery.LocalData
{
    internal class Kunde
    {
        public int Kundennummer { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public Adresse addresse;

        public Kunde() { }

        public string getNameString()
        {
            return Vorname + " " + Nachname;
        }
    }
}
