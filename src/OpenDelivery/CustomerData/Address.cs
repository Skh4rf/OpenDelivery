using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDelivery.CustomerData
{
    internal class Address
    {
        public int Postalcode { get; private set; }
        public string City { get; private set;}
        public string Street { get; private set; }
        public int Number { get; private set; }
        public int Number2 { get; private set; }
        public int Latitude { get; private set; }
        public int Longitude { get; private set; }

        public Address() { }
    }
}
