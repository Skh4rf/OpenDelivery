using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace OpenDelivery.Services
{
    internal class Geocoding
    {
        public static async Task<MapLocationFinderResult> GeocodeAnAddress(string address, int address_id)
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

            return result;
        }
    }
}
