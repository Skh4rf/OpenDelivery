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
        public Adresse adresse;

        public Kunde() { }

        public Kunde(string vorname, string nachname, Adresse adrs)
        {
            Vorname = vorname;
            Nachname = nachname;
            adresse = adrs;
        }

        public string getNameString()
        {
            return Vorname + " " + Nachname;
        }
    }
}
