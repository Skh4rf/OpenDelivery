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
            if (LocalData.Container.Kunden != null)
            { 
                if (ComboBoxCustomerSelect.Items.Count != 0)
                {
                    ComboBoxCustomerSelect.Items.Clear();
                }
                foreach (LocalData.Kunde k in LocalData.Container.Kunden)
                {
                    ComboBoxCustomerSelect.Items.Add(k.getNameString());
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Services.Database.refreshData();
            RefreshComboBox();
        }

        private void ButtonCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            Services.Database.refreshData();
            RefreshComboBox();
            /*LocalData.Koordinate koordinate = Services.Database.createTable(new LocalData.Koordinate(2, 2));
            LocalData.Adresse adresse = Services.Database.createTable(new LocalData.Adresse(6850, "Dornbirn", "Stockach", 6, "", koordinate));
            LocalData.Kunde kunde = Services.Database.createTable(new LocalData.Kunde("Jakob", "Metzler", adresse));
            LocalData.Route route = Services.Database.createTable(new LocalData.Route("TestTour"));
            LocalData.BestelltesProdukt produkt1 = new LocalData.BestelltesProdukt();
            produkt1.Einheit = "Liter";
            produkt1.Name = "Milch";
            produkt1.Menge = 1;
            List<LocalData.BestelltesProdukt> produkte = new List<LocalData.BestelltesProdukt>();
            produkte.Add(produkt1);
            LocalData.Bestellung bestellung = Services.Database.createTable(new LocalData.Bestellung(produkte, kunde, route));*/
        }
    }
}
