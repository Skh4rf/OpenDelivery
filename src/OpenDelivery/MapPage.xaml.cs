using OpenDelivery.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;


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

        #region MapInitialization
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

        public async void ManuelInitialization()
        {
            await InitializeAsync(mapControl);
        }

        #endregion MapIntialization

        #region LoadRoute
        private async void LoadRoute(double lat, double lon, MapRouteOptimization optimization = MapRouteOptimization.Time, MapRouteRestrictions restrictions = MapRouteRestrictions.None)
        {
            BasicGeoposition basicGeoposition = new BasicGeoposition();
            basicGeoposition.Latitude = lat;
            basicGeoposition.Longitude = lon;
            Geopoint geopoint = new Geopoint(basicGeoposition);

            await _gpsService.CalculateRouteAsync(geopoint, optimization, restrictions);

            if (_gpsService.Route.Status == MapRouteFinderStatus.Success)
            {
                MapRouteView viewOfRoute = new MapRouteView(_gpsService.Route.Route);
                viewOfRoute.RouteColor = Colors.DarkMagenta;
                viewOfRoute.OutlineColor = Colors.DarkMagenta;

                mapControl.Routes.Add(viewOfRoute);

                await mapControl.TrySetViewBoundsAsync(
                      _gpsService.Route.Route.BoundingBox,
                      null,
                      MapAnimationKind.Bow);
                await mapControl.TrySetViewAsync(_gpsService.Route.Route.Legs[0].Maneuvers[0].StartingPoint, 20);

                mapControl.Heading = _gpsService.Route.Route.Legs[0].Maneuvers[0].StartHeading;
                await mapControl.TryTiltAsync(45);
            }
            else
            {
                throw new Exception("Could not calculate a route!");
            }
        }

        private async void LoadRoute(Geopoint destination, MapRouteOptimization optimization = MapRouteOptimization.Time, MapRouteRestrictions restrictions = MapRouteRestrictions.None)
        {
            await _gpsService.CalculateRouteAsync(destination, optimization, restrictions);

            if (_gpsService.Route.Status == MapRouteFinderStatus.Success)
            {
                MapRouteView viewOfRoute = new MapRouteView(_gpsService.Route.Route);
                viewOfRoute.RouteColor = Colors.DarkMagenta;
                viewOfRoute.OutlineColor = Colors.DarkMagenta;

                mapControl.Routes.Add(viewOfRoute);

                await mapControl.TrySetViewBoundsAsync(
                      _gpsService.Route.Route.BoundingBox,
                      null,
                      MapAnimationKind.Bow);
                await mapControl.TrySetViewAsync(_gpsService.Route.Route.Legs[0].Maneuvers[0].StartingPoint, 20);

                mapControl.Heading = _gpsService.Route.Route.Legs[0].Maneuvers[0].StartHeading;
                await mapControl.TryTiltAsync(45);
            }
            else
            {
                throw new Exception("Could not calculate a route!");
            }
        }
        #endregion LoadRoute

        public void RouteSelected()
        {
            LocalData.Container.CurrentBestellungen = LocalData.Container.Bestellungen.Where(bestellung => bestellung.route.RoutenID == LocalData.Container.CurrentRoute.RoutenID).ToList();
            LocalData.Koordinate koord = LocalData.Container.CurrentBestellungen[LocalData.Container.CurrentRoutePosition].kunde.adresse.koordinate;

            GridCustomerInfo.Visibility = Windows.UI.Xaml.Visibility.Visible;

            LoadCustomerInfo1(LocalData.Container.CurrentBestellungen[0]);

            if (LocalData.Container.CurrentBestellungen.Count > 1) { LoadCustomerInfo2(LocalData.Container.CurrentBestellungen[1]); }

            LoadRoute(koord.Latitude, koord.Longitude);
        }

        private void LoadCustomerInfo1(LocalData.Bestellung bestellung)
        {
            TextBlockCustomerInfo1_Name.Text = bestellung.kunde.getNameString();
            TextBlockCustomerInfo1_Adresse.Text = bestellung.kunde.adresse.getFullString();
            TextBlockCustomerInfo1_Products.Text = String.Empty;
            foreach (LocalData.BestelltesProdukt p in bestellung.Produkte)
            {
                TextBlockCustomerInfo1_Products.Text += p.ToString() + Environment.NewLine;
            }
        }

        private void LoadCustomerInfo2(LocalData.Bestellung bestellung)
        {
            TextBlockCustomerInfo2_Name.Text = bestellung.kunde.getNameString();
            TextBlockCustomerInfo2_Adresse.Text = bestellung.kunde.adresse.getFullString();
            TextBlockCustomerInfo2_Products.Text = String.Empty;
            foreach (LocalData.BestelltesProdukt p in bestellung.Produkte)
            {
                TextBlockCustomerInfo2_Products.Text += p.ToString() + Environment.NewLine;
            }
        }

        public void RouteStopped()
        {
            mapControl.Routes.Clear();
        }
    }
}
