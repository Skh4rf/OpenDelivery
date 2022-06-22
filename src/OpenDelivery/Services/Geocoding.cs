using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace OpenDelivery.Services
{
    internal class Geocoding
    {
        public static async void GeocodeAnAddress(string address, int address_id)
        {
            string addressToGeocode = $"{address}";


            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 47.45020000;  // Alberschwende als Such-Anhaltspunkt
            queryHint.Longitude = 9.83039000;
            Geopoint hintPoint = new Geopoint(queryHint);

            MapLocationFinderResult result =
                await MapLocationFinder.FindLocationsAsync(
                addressToGeocode,
                hintPoint,
                3);

            if (result.Status == MapLocationFinderStatus.Success)
            {
                // Koordinaten auf Datenbank laden
            }
        }
    }
}
