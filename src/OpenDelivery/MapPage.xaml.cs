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

        private void TryLoadCustomerInfos()
        {
            if (LocalData.Container.CurrentBestellungen.Count != LocalData.Container.CurrentRoutePosition + 1)
            {
                GridCustomerInfo2.Visibility = Windows.UI.Xaml.Visibility.Visible;
                LoadCustomerInfo2(LocalData.Container.CurrentBestellungen[LocalData.Container.CurrentRoutePosition + 1]);
            }
            else { GridCustomerInfo2.Visibility = Windows.UI.Xaml.Visibility.Collapsed; }
            LoadCustomerInfo1(LocalData.Container.CurrentBestellungen[LocalData.Container.CurrentRoutePosition]);
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

        public async void RouteStopped()
        {
            mapControl.Routes.Clear();
            await mapControl.TryTiltToAsync(0);
        
            mapControl.ZoomLevel = DefaultZoomLevel;
            GridConfirm.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            GridCustomerInfo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void ButtonNextCustomer_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GridConfirm.Visibility = Windows.UI.Xaml.Visibility.Visible;
            LoadProductConfirm();
        }

        private void LoadProductConfirm()
        {
            GridConfirm.Visibility = Windows.UI.Xaml.Visibility.Visible;
            ComboBoxConfirmItem.Items.Clear();
            foreach (LocalData.BestelltesProdukt p in LocalData.Container.CurrentBestellungen[LocalData.Container.CurrentRoutePosition].Produkte)
            {
                ComboBoxConfirmItem.Items.Add(p.Name.ToString());
            }
        }

        private void StackpanelConfirm_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            RectangleConfirm.Height = StackpanelConfirm.ActualHeight;
        }

        private void ButtonConfirm_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Database.createTable(LocalData.Container.CurrentBestellungen[LocalData.Container.CurrentRoutePosition].kunde, 
                JSON.ProduktListToJson(LocalData.Container.CurrentBestellungen[LocalData.Container.CurrentRoutePosition].Produkte));
            if (LocalData.Container.CurrentBestellungen.Count != LocalData.Container.CurrentRoutePosition + 1)
            {
                LocalData.Container.CurrentRoutePosition++;
            }
            GridConfirm.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            TryLoadCustomerInfos();
        }

        private void ComboBoxConfirmItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxConfirmItem.SelectedItem != null)
            {
                LocalData.BestelltesProdukt bestelltesProdukt = LocalData.Container.CurrentBestellungen[LocalData.Container.CurrentRoutePosition].Produkte.Single(
                                produkt => produkt.Name.Equals(ComboBoxConfirmItem.SelectedItem.ToString()));
                TextBlockConfirmUnit.Text = bestelltesProdukt.Einheit;
                TextBoxConfirmQuantity.Text = bestelltesProdukt.Menge.ToString();
            }
        }

        private void ButtonSaveConfirmChang_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LocalData.Container.CurrentBestellungen[LocalData.Container.CurrentRoutePosition].Produkte[
                LocalData.Container.CurrentBestellungen[LocalData.Container.CurrentRoutePosition].Produkte.FindIndex(
                    produkt => produkt.Name.Equals(ComboBoxConfirmItem.SelectedItem.ToString()))].Menge =
                    Convert.ToInt32(TextBoxConfirmQuantity.Text);
        }
    }
}
