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
            if (ComboBoxRouteSelect.Items.Count != 0)
            {
                ComboBoxRouteSelect.Items.Clear();
            }
            foreach (string str in Services.Database.routeNames)
            {
                ComboBoxRouteSelect.Items.Add(str);
                ComboBoxRouteSelect.SelectedIndex = 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Services.Database.refreshData();
            RefreshComboBox();
        }
    }
}
