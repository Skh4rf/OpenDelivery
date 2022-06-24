using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace OpenDelivery.Services
{
    public class GPS
    {
        private Geolocator _geolocator;

        public event EventHandler<Geoposition> OnPositionChanged;

        public Geoposition CurrentPosition { get; set; }

        public MapRouteFinderResult Route { get; set; }


        #region initialization
        //Aufbau eines Call-Tree zur Ermittlung der Position unter berücksichtigung verschiedener Parameter --> modularität
        public Task<bool> InitializeAsync() { return InitializeAsync(50); }

        public Task<bool> InitializeAsync(uint accuracy)
        {
            return InitializeAsync(accuracy, (double)accuracy / 2);
        }

        public async Task<bool> InitializeAsync(uint accuracy, double movementThreshold)
        {
            if (_geolocator != null)
            {
                _geolocator.PositionChanged -= Geolocator_PositionChanged; //Automatische Standortabfrage deaktivieren
                _geolocator = null;
            }

            var access = await Geolocator.RequestAccessAsync();

            bool result;

            switch (access)
            {
                case GeolocationAccessStatus.Allowed:
                    _geolocator = new Geolocator()
                    {
                        DesiredAccuracyInMeters = accuracy,
                        MovementThreshold = movementThreshold
                    };
                    result = true;
                    break;
                case GeolocationAccessStatus.Unspecified:
                case GeolocationAccessStatus.Denied:
                default:
                    result = false;
                    break;
            }

            return result;
        }
        #endregion initialization

        #region positiondetection
        public async Task StartListeningAsnyc() // Automatische Standortabfrage aktivieren
        {
            if (_geolocator == null) { await InitializeAsync(); }

            _geolocator.PositionChanged += Geolocator_PositionChanged;

            CurrentPosition = await _geolocator.GetGeopositionAsync();
        }

        public void StopListening() // Automatische Standortabfrage deaktivieren
        {
            if (_geolocator == null) { return; }

            _geolocator.PositionChanged -= Geolocator_PositionChanged;
        }

        private async void Geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args) //Erstellen eines Dispatchers zur regelmäßigen Standortüberprüfung
        {
            if (args == null) { return; }

            CurrentPosition = args.Position;

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                OnPositionChanged?.Invoke(this, CurrentPosition);
            });
        }
        #endregion positiondetection

        #region routecalculation

        public async Task CalculateRouteAsync(Geopoint destination, MapRouteOptimization optimization, MapRouteRestrictions restrictions)
        {
            // Aktueller Standort ermitteln
            if (_geolocator == null) { await InitializeAsync(); }
            CurrentPosition = await _geolocator.GetGeopositionAsync();
            //Exception potential
            Route = await MapRouteFinder.GetDrivingRouteAsync(
                    new Geopoint(CurrentPosition.Coordinate.Point.Position),
                    destination,
                    optimization,
                    restrictions);
        }

        #endregion routecalculation
    }
}
