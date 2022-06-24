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
using OpenDelivery.LocalData;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OpenDelivery
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
            }
        }

        private void LoadRoute_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxRouteSelect.SelectedItem != null)
            {
                Container.CurrentRoute = Container.Routen.Single(route => route.Name.Equals(ComboBoxRouteSelect.SelectedValue));
                Container.CurrentRoutePosition = 0;
            }
        }
    }
}
