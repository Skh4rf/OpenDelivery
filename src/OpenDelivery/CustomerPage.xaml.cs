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
    public sealed partial class CustomerPage : Page
    { 
        public CustomerPage()
        {
            this.InitializeComponent();
        }

        public void RefreshComboBox()
        {
            if (ComboBoxCustomerSelect.Items.Count != 0)
            {
                ComboBoxCustomerSelect.Items.Clear();
            }
            foreach(string str in Services.Database.customer)
            {
                ComboBoxCustomerSelect.Items.Add(str);
                ComboBoxCustomerSelect.SelectedIndex = 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Services.Database.refreshData();
            RefreshComboBox();
            
        }
    }
}
