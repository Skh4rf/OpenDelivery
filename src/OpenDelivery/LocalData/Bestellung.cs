using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDelivery.LocalData
{
    internal class Bestellung
    {
        public Customer Customer { get; private set; }
        public List<Produkt> Produkt { get; private set; }

        public Bestellung() { Produkt = new List<Produkt>(); }
    }
}
