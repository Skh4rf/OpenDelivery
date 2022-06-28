using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace OpenDelivery.Services
{
    internal class Geocoding
    {
        public static async Task<MapLocationFinderResult> GeocodeAnAddress(string address)
        {
            string addressToGeocode = address;


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

       public static async void GeoCodeAnAddressToKoordinate(string address)
        {
            string addressToGeocode = address;


            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 47.45020000;  // Alberschwende als Such-Anhaltspunkt
            queryHint.Longitude = 9.83039000;
            Geopoint hintPoint = new Geopoint(queryHint);

            MapLocationFinderResult result =
                await MapLocationFinder.FindLocationsAsync(
                addressToGeocode,
                hintPoint,
                3);

            LocalData.Koordinate resultCoord = new LocalData.Koordinate();

            if (result.Status == MapLocationFinderStatus.Success)
            {
                resultCoord.Longitude = (float)result.Locations[0].Point.Position.Longitude;
                resultCoord.Latitude = (float)result.Locations[0].Point.Position.Latitude;
            }
            LocalData.Container.geocodingdump = resultCoord;
        }
    }
}
