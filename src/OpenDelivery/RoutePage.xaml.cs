using OpenDelivery.LocalData;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


namespace OpenDelivery
{
    public sealed partial class RoutePage : Page
    {

        public RoutePage()
        {
            this.InitializeComponent();

        }

        public void RefreshComboBox()
        {
            if (LocalData.Container.Routen != null)
            {
                if (ComboBoxRouteSelect.Items.Count != 0)
                {
                    ComboBoxRouteSelect.Items.Clear();
                }
                foreach (Route r in Container.Routen)
                {
                    ComboBoxRouteSelect.Items.Add(r.Name);
                    ComboBoxRouteSelect.SelectedIndex = -1;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Services.Database.refreshData();
            RefreshComboBox();
        }

        private void ComboBoxRouteSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxRouteSelect.SelectedItem != null)
            {
                if (GridRouteListing.Children.Count > 0) { GridRouteListing.Children.Clear(); }
                GridRouteListing.Children.Add(Services.RouteListing.getStackPanelForRoute(Container.Routen.Single(route => route.Name.Equals(ComboBoxRouteSelect.SelectedValue))));
                LoadRoute.IsEnabled = true;
            }
            else
            {
                LoadRoute.IsEnabled = false;
            }
        }

        private void LoadRoute_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxRouteSelect.SelectedItem != null)
            {
                if (LoadRoute.Content.Equals("Start"))
                {
                    Container.CurrentRoute = Container.Routen.Single(route => route.Name.Equals(ComboBoxRouteSelect.SelectedValue));
                    Container.CurrentRoutePosition = 0;
                    LoadRoute.Content = "Stop";
                    LoadRoute.Background = new SolidColorBrush(Colors.Red);
                    LoadRoute.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                    ComboBoxRouteSelect.IsEnabled = false;
                    ButtonReloadDatabase.IsEnabled = false;
                    ButtonCreateRoute.IsEnabled = false;
                }
                else
                {
                    Container.CurrentRoute = null;
                    Container.CurrentRoutePosition = 0;
                    Container.CurrentBestellungen = null;
                    LoadRoute.Content = "Start";
                    LoadRoute.Background = new SolidColorBrush(Colors.Orange);
                    LoadRoute.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
                    ComboBoxRouteSelect.IsEnabled = true;
                    ButtonReloadDatabase.IsEnabled = true;
                    ButtonCreateRoute.IsEnabled = true;
                }

            }
        }
    }
}
