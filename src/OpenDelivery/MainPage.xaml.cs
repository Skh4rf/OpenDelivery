using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace OpenDelivery
{
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
            }else if(LocalData.Container.CurrentRoute == null)
            {
                mapPage.RouteStopped();
            }
        }

        private void AppBarButtonRouten_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPageView.Children.Clear();
            this.CurrentPageView.Children.Add(routePage);
            if (LocalData.Container.CurrentRoute == null)
            {
                routePage.RefreshComboBox();
            }
        }

        private void AppBarButtonKunden_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPageView.Children.Clear();
            this.CurrentPageView.Children.Add(customerPage);
            customerPage.RefreshComboBox();
            if (LocalData.Container.CurrentRoute != null)
            {
                customerPage.DisableButtons();
            }
            else
            {
                customerPage.EnableButtons();
            }

        }

        private void AppBarRechnung_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPageView.Children.Clear();
            this.CurrentPageView.Children.Add(invoicePage);
        }
    }
}
