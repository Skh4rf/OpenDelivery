using System.Collections.Generic;

namespace OpenDelivery.LocalData
{
    internal static class Container
    {
        public static List<Bestellung> Bestellungen { get; set; }
        public static List<Koordinate> Koordinaten { get; set; }
        public static List<Adresse> Adressen { get; set; }
        public static List<Kunde> Kunden { get; set; }
        public static List<Route> Routen { get; set; }
        public static List<Produkt> produkte { get; set; }

        public static Route CurrentRoute { get; set; }
        public static int CurrentRoutePosition { get; set; }
        public static List<Bestellung> CurrentBestellungen { get; set; }
    }
}
