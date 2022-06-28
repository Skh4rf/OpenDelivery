using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;


namespace OpenDelivery
{
    public sealed partial class CustomerPage : Page
    {
        private List<LocalData.Bestellung> _bestellungen;

        private LocalData.Kunde createKunde;
        private LocalData.Adresse createAdresse;
        private LocalData.Koordinate createKoordinate;
        private List<LocalData.Bestellung> createBestellungen;
        private LocalData.Bestellung createBestellung;

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

        private void ComboBoxCustomerSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxSelectRoute.Items.Clear();
            TextBlockProdukte.Text = String.Empty;
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
            TextBoxChange1.Text = _bestellungen.First().kunde.Vorname;
            TextBoxChange2.Text = _bestellungen.First().kunde.Nachname;
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
            Services.Database.updateValue("Kunden", "Kundennummer", _bestellungen.First().kunde.Kundennummer.ToString(), "Vorname", $"\"{TextBoxChange1.Text}\"");
            Services.Database.updateValue("Kunden", "Kundennummer", _bestellungen.First().kunde.Kundennummer.ToString(), "Nachname", $"\"{TextBoxChange2.Text}\"");
            Services.Database.refreshData();
            RefreshComboBox();
            ButtonEditAdresse.IsEnabled = ButtonEditName.IsEnabled = ButtonEditProdukte.IsEnabled = ButtonAddRoute.IsEnabled = ButtonDeleteRoute.IsEnabled = true;
            GridEditName.Visibility = Visibility.Collapsed;
            GridShowCustomer.Visibility = Visibility.Collapsed;
        }

        private void ButtonCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            GridCreateNewCustomer.Visibility = Visibility.Visible;
            createKunde = new LocalData.Kunde();
            createAdresse = new LocalData.Adresse();
            createKoordinate = new LocalData.Koordinate();
            createBestellungen = new List<LocalData.Bestellung>();
            createBestellung = new LocalData.Bestellung();

            ComboBoxCreateCustomerRoute.Items.Clear();
            foreach(LocalData.Route r in LocalData.Container.Routen)
            {
                ComboBoxCreateCustomerRoute.Items.Add(r.Name);
            }

            ComboBoxCreateCustomerProducts.Items.Clear();
            foreach(LocalData.Produkt p in LocalData.Container.produkte)
            {
                ComboBoxCreateCustomerProducts.Items.Add(p.Name);
            }
        }

        private void TextBoxCreateCustomerFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Regex
        }

        private void TextBoxCreateCustomerLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Regex
        }

        private void TextBoxCreateCustomerStreet_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Regex
        }

        private void TextBoxCreateCustomerNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Regex
        }

        private void TextBoxCreateCustomerNumber2_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Regex
        }

        private void TextBoxCreateCustomerPostalcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Regex
        }

        private void TextBoxCreateCustomerCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Regex
        }

        private void TextBoxCreateCustomerProductQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Regex

            if(TextBoxCreateCustomerProductQuantity.Text.Length > 0 && ComboBoxCreateCustomerProducts.SelectedItem != null)
            {
                ButtonCreateCustomerAddProduct.IsEnabled = true;
            }
            else { ButtonCreateCustomerAddProduct.IsEnabled = false; }
        }

        private void ComboBoxCreateCustomerRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxCreateCustomerRoute.SelectedItem != null)
            {
                StackPanelCreateCustomerAddProducts.Visibility = Visibility.Visible;
            }
            else
            {
                StackPanelCreateCustomerAddProducts.Visibility = Visibility.Collapsed;
            }
        }

        private void ComboBoxCreateCustomerProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TextBoxCreateCustomerProductQuantity.Text.Length > 0 && ComboBoxCreateCustomerProducts.SelectedItem != null)
            {
                ButtonCreateCustomerAddProduct.IsEnabled = true;
                TextBlockCreateCustomerProductUnit.Text = LocalData.Container.produkte.Single(p => p.Name.Equals(ComboBoxCreateCustomerProducts.SelectedItem.ToString())).Einheit;
            }
            else { ButtonCreateCustomerAddProduct.IsEnabled = false; }
        }

        private void ButtonCreateCustomerAddRoute_Click(object sender, RoutedEventArgs e)
        {
            createBestellung.route = LocalData.Container.Routen.Single(r => r.Name.Equals(ComboBoxCreateCustomerRoute.SelectedItem.ToString()));
            TextBlockCreateCustomerSelectedRoute.Text += $"- {createBestellung.route.Name}{Environment.NewLine}";

            TextBlockCreateCustomerShowSelectedProducts.Text = "";
            createBestellung = new LocalData.Bestellung();

            StackPanelCreateCustomerAddProducts.Visibility = Visibility.Collapsed;
        }

        private void ButtonCreateCustomerAddProduct_Click(object sender, RoutedEventArgs e)
        {
            LocalData.BestelltesProdukt bestelltesProdukt = new LocalData.BestelltesProdukt();
            LocalData.Produkt produkt = LocalData.Container.produkte.Single(p => p.Name.Equals(ComboBoxCreateCustomerProducts.SelectedItem.ToString()));
            
            // +++ evtl bestelltesProdukt Kunstruktor machen
            bestelltesProdukt.Name = produkt.Name;
            bestelltesProdukt.Artikelnummer = produkt.Artikelnummer;
            bestelltesProdukt.Einheit = produkt.Einheit;
            bestelltesProdukt.Menge = Convert.ToDouble(TextBoxCreateCustomerProductQuantity.Text); // +++ Exception

            createBestellung.Produkte.Add(bestelltesProdukt);

            TextBlockCreateCustomerShowSelectedProducts.Text += $"- {bestelltesProdukt.Menge} {bestelltesProdukt.Einheit} {bestelltesProdukt.Name} {Environment.NewLine}";
        }

        private void TextBlockCreateCustomerSave_Click(object sender, RoutedEventArgs e)
        {
            createAdresse.Ort = TextBoxCreateCustomerCity.Text;
            createAdresse.Postleitzahl = Convert.ToInt32(TextBoxCreateCustomerPostalcode.Text);
            createAdresse.Strasse = TextBoxCreateCustomerStreet.Text;
            createAdresse.Nummer = Convert.ToInt32(TextBoxCreateCustomerNumber.Text);
            if (TextBoxCreateCustomerNumber2.Text.Length > 0) { createAdresse.Adresszusatz = TextBoxCreateCustomerNumber2.Text; }
            //createAdresse.koordinate = getCoords();

            createKunde.Vorname = TextBoxCreateCustomerFirstName.Text;  
            createKunde.Nachname = TextBoxCreateCustomerLastName.Text;
            createKunde.adresse = createAdresse;

            foreach(LocalData.Bestellung b in createBestellungen)
            {
                b.kunde = createKunde;
            }

            GridCreateNewCustomer.Visibility = Visibility.Collapsed;
        }

        private LocalData.Koordinate getCoords()
        {
            Services.Geocoding.GeoCodeAnAddressToKoordinate(createAdresse.getFullStringWithoutAdditionalNumber());

            return LocalData.Container.geocodingdump;
        }
    }
}
