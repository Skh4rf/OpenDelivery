using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDelivery.LocalData
{
    internal class Produkt
    {
        public int Artikelnummer { get; set; }
        public string Name { get; set; }
        public string Einheit { get; set; }

        public Produkt() { }
    }
}
