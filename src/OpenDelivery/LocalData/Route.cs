using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDelivery.LocalData
{
    internal class Route
    {
        public List<Bestellung> Bestellungen { get; private set; }

        public Route() { Bestellungen = new List<Bestellung>(); }
    }
}
