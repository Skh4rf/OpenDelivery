using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDelivery.CustomerData
{
    internal class Customer
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address Address { get; private set; }

        public Customer() { }
    }
}
