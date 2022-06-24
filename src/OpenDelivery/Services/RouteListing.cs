using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using Windows.UI;
using OpenDelivery.LocalData;

namespace OpenDelivery.Services
{
    internal static class RouteListing
    {
        public static StackPanel getStackPanelForRoute(Route route)
        {
            StackPanel RouteListing = new StackPanel();

            int count = 1;

            foreach (Bestellung b in Container.Bestellungen.Where(bestellung => bestellung.route == route).ToList())
            {
                StackPanel customerStackPanel = new StackPanel();
                customerStackPanel.Orientation = Orientation.Horizontal;

                TextBlock countIndex = new TextBlock();
                countIndex.Text = count.ToString();
                countIndex.FontSize = 48;
                countIndex.VerticalAlignment = VerticalAlignment.Top;
                countIndex.HorizontalAlignment = HorizontalAlignment.Center;

                StackPanel customerInformation = new StackPanel();
                customerInformation.Margin = new Thickness(40, 0, 0, 0);

                TextBlock customerName = new TextBlock();
                customerName.Text = b.kunde.getNameString();
                customerName.FontSize = 48;
                customerName.VerticalAlignment = VerticalAlignment.Bottom;
                customerName.HorizontalAlignment = HorizontalAlignment.Left;

                TextBlock customerAdresse1 = new TextBlock();
                customerAdresse1.Text = b.kunde.adresse.getCityString();
                customerAdresse1.FontSize = 24;
                customerAdresse1.VerticalAlignment = VerticalAlignment.Bottom;
                customerAdresse1.HorizontalAlignment = HorizontalAlignment.Left;

                TextBlock customerAdresse2 = new TextBlock();
                customerAdresse2.Text = b.kunde.adresse.getStreetString();
                customerAdresse2.FontSize = 24;
                customerAdresse2.VerticalAlignment = VerticalAlignment.Bottom;
                customerAdresse2.HorizontalAlignment = HorizontalAlignment.Left;

                StackPanel itemsStackPanel = new StackPanel();
                itemsStackPanel.Margin = new Thickness(70, 0, 0, 0);
                itemsStackPanel.VerticalAlignment = VerticalAlignment.Bottom;
                itemsStackPanel.HorizontalAlignment = HorizontalAlignment.Center;

                foreach(BestelltesProdukt p in b.Produkte)
                {
                    TextBlock produkt = new TextBlock();
                    produkt.Text = p.Menge + " " + p.Einheit + " " + p.Name;
                    produkt.FontSize = 24;
                    itemsStackPanel.Children.Add(produkt);
                }

                customerInformation.Children.Add(customerName);
                customerInformation.Children.Add(customerAdresse1);
                customerInformation.Children.Add(customerAdresse2);

                customerStackPanel.Children.Add(countIndex);
                customerStackPanel.Children.Add(customerInformation);
                customerStackPanel.Children.Add(itemsStackPanel);

                Line line = new Line();
                line.Stroke = new SolidColorBrush(Colors.White);
                line.X1 = 0;
                line.X2 = 1300;
                line.StrokeThickness = 3;
                line.HorizontalAlignment = HorizontalAlignment.Left;
                line.Margin = new Thickness(0, 10, 0, 10);

                RouteListing.Children.Add(customerStackPanel);
                RouteListing.Children.Add(line);
            }

            return RouteListing;
        }
    }
}
