using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDelivery.LocalData
{
    internal class Bestellung
    {
        public int Bestellnummer { get; set; }
        public List<BestelltesProdukt> Produkte { get; set; }
        public Kunde kunde;
        public Route route;

        public Bestellung() { Produkte = new List<BestelltesProdukt>(); }

        public Bestellung(List<BestelltesProdukt> produkte, Kunde k, Route r)
        {
            Produkte = produkte;
            kunde = k;
            route = r;
        }
    }
}
