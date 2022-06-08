using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using OpenDelivery.Services;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OpenDelivery
{
    public sealed partial class MapPage : Page
    {
        private const double DefaultZoomLevel = 17;

        private readonly GPS _gpsService;

        private readonly BasicGeoposition _defaultPosition = new BasicGeoposition() { Latitude = 0, Longitude = 0 };

        public double ZoomLevel { get; set; }

        public Geopoint Center { get; set; }

        public MapPage()
        {
            this.InitializeComponent();
            _gpsService = new GPS();
            Center = new Geopoint(_defaultPosition);
            ZoomLevel = DefaultZoomLevel;
        }

        public async Task InitializeAsync(MapControl map)
        {
            if (_gpsService != null)
            {
                _gpsService.OnPositionChanged += GpsService_PositionChanged;

                var initializationCheck = await _gpsService.InitializeAsync();

                if (initializationCheck)
                {
                    await _gpsService.StartListeningAsnyc();
                }

                if (initializationCheck && _gpsService.CurrentPosition != null)
                {
                    Center = _gpsService.CurrentPosition.Coordinate.Point;
                }
                else
                {
                    Center = new Geopoint(_defaultPosition);
                }
            }
        }

        private void GpsService_PositionChanged(object sender, Geoposition geoposition)
        {
            if (geoposition != null) { Center = geoposition.Coordinate.Point; }
            mapControl.Center = Center;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await InitializeAsync(mapControl);
        }


    }
}
