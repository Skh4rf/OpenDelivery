using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDelivery.LocalData
{
    internal class Produkt
    {
        public string Name { get; private set; }
        public string Einheit { get; private set; }
        public double Menge { get; private set; }

        public Produkt() { }
    }
}
