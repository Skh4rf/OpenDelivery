using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace OpenDelivery
{
    public sealed partial class CustomerPage : Page
    {
        private List<LocalData.Bestellung> _bestellungen;

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

        public void DisableButtons()
        {
            ButtonLoadDatabase.IsEnabled = false;
            ButtonCreateCustomer.IsEnabled = false;
        }

        public void EnableButtons()
        {
            ButtonLoadDatabase.IsEnabled = true;
            ButtonCreateCustomer.IsEnabled = true;
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
        }

        private void ComboBoxCustomerSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxSelectRoute.Items.Clear();
            GridShowCustomer.Visibility = Visibility.Collapsed;
            if (ComboBoxCustomerSelect.SelectedItem != null)
            {
                GridShowCustomer.Visibility = Visibility.Visible;
                LocalData.Kunde kunde = LocalData.Container.Kunden.Single(k => $"{k.Vorname} {k.Nachname}".Equals(ComboBoxCustomerSelect.SelectedItem.ToString()));
                _bestellungen = LocalData.Container.Bestellungen.Where(bestellung => bestellung.kunde.Kundennummer == kunde.Kundennummer).ToList();


                TextBlockVorname.Text = $"{kunde.Vorname} {kunde.Nachname}";
                TextBlockStreet.Text = kunde.adresse.getStreetString();
                TextBlockCity.Text = kunde.adresse.getCityString();

                foreach (LocalData.Bestellung bestellung in _bestellungen)
                {
                    ComboBoxSelectRoute.Items.Add(bestellung.route.Name);
                }
            }
        }

        private void ComboBoxSelectRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxSelectRoute.SelectedItem != null)
            {
                TextBlockProdukte.Text = "";
                LocalData.Bestellung bestellung = _bestellungen.Single(b => b.route.Name.Equals(ComboBoxSelectRoute.SelectedItem.ToString()));
                foreach (LocalData.BestelltesProdukt p in bestellung.Produkte)
                {
                    TextBlockProdukte.Text += $"- {p.Menge} {p.Einheit} {p.Name} {Environment.NewLine}";
                }
            }
        }

        private void ButtonEditName_Click(object sender, RoutedEventArgs e)
        {
            TextBoxChangeFirstName.Text = _bestellungen.First().kunde.Vorname;
            TextBoxChangeLastName.Text = _bestellungen.First().kunde.Nachname;
            ButtonEditAdresse.IsEnabled = ButtonEditName.IsEnabled = ButtonEditProdukte.IsEnabled = ButtonAddRoute.IsEnabled = ButtonDeleteRoute.IsEnabled = false;
            GridEditName.Visibility = Visibility.Visible;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            ButtonEditAdresse.IsEnabled = ButtonEditName.IsEnabled = ButtonEditProdukte.IsEnabled = ButtonAddRoute.IsEnabled = ButtonDeleteRoute.IsEnabled = true;
            GridEditName.Visibility = Visibility.Collapsed;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Services.Database.updateValue("Kunden", "Kundennummer", _bestellungen.First().kunde.Kundennummer.ToString(), "Vorname", $"\"{TextBoxChangeFirstName.Text}\"");
            Services.Database.refreshData();
            RefreshComboBox();
            ButtonEditAdresse.IsEnabled = ButtonEditName.IsEnabled = ButtonEditProdukte.IsEnabled = ButtonAddRoute.IsEnabled = ButtonDeleteRoute.IsEnabled = true;
            GridEditName.Visibility = Visibility.Collapsed;
            GridShowCustomer.Visibility = Visibility.Collapsed;
        }
    }
}
