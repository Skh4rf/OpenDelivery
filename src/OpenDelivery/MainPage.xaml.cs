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
using System.Threading;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OpenDelivery
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MapPage mapPage;
        private RoutePage routePage = new RoutePage();
        private CustomerPage customerPage = new CustomerPage();
        private InvoicePage invoicePage = new InvoicePage();

        public MainPage()
        {
            this.InitializeComponent();
            
        }

        #region Initialisierung


        #endregion

        private void AppBarButtonMap_Click(object sender, RoutedEventArgs e)
        {
            if (mapPage == null)
            {
                mapPage = new MapPage();
                mapPage.ManuelInitialization();
            }
            this.CurrentPageView.Children.Clear();
            this.CurrentPageView.Children.Add(mapPage);
            if (LocalData.Container.CurrentRoute != null && LocalData.Container.CurrentRoutePosition == 0)
            {
                mapPage.RouteSelected();
            }
        }

        private void AppBarButtonRouten_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPageView.Children.Clear();
            this.CurrentPageView.Children.Add(routePage);
            routePage.RefreshComboBox();
        }

        private void AppBarButtonKunden_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPageView.Children.Clear();
            this.CurrentPageView.Children.Add(customerPage);
            customerPage.RefreshComboBox();
        }

        private void AppBarRechnung_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPageView.Children.Clear();
            this.CurrentPageView.Children.Add(invoicePage);
        }
    }
}
