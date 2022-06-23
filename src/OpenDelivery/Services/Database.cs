using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace OpenDelivery.Services
{
    internal class Database
    {
        protected static MySqlConnection con_delivery = new MySqlConnection(@"server=localhost;userid=root;password=;database=opendelivery");

        public static List<string> routeNames { get; set; } = new List<string>();
        public static List<string> customer { get; set; } = new List<string>();

        public static void refreshData()
        {
            //routeNames.Clear();
            //customer.Clear();
            //getRoutes();
            //getCustomerNames();
            getKoordinaten();
            getAdressen();
            getKunden();
            getRouten();
        }

        public static void getKoordinaten()
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM koordinaten", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<LocalData.Koordinate> result = new List<LocalData.Koordinate>();

            LocalData.Koordinate koordinate;

            while (reader.Read())
            {
                koordinate = new LocalData.Koordinate();
                koordinate.KoordinatenID = reader.GetInt32(0);
                koordinate.Latitude = reader.GetFloat(1);
                koordinate.Longitude = reader.GetFloat(2);
                result.Add(koordinate);
            }

            LocalData.Container.Koordinaten = result;
            reader.Close();
            con_delivery.Close();
        }

        public static void getAdressen()
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM adressen", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<LocalData.Adresse> result = new List<LocalData.Adresse>();

            LocalData.Adresse adresse;

            while (reader.Read())
            {
                adresse = new LocalData.Adresse();
                adresse.Adressnummer = reader.GetInt32(0);
                adresse.Postleitzahl = reader.GetInt32(1);
                adresse.Strasse = reader.GetString(2);
                adresse.Nummer = reader.GetInt32(3);
                try
                {
                    adresse.Adresszusatz = reader.GetString(4);
                }catch (Exception ex) { }
                adresse.koordinate = LocalData.Container.Koordinaten.Single(koordinate => koordinate.KoordinatenID == reader.GetInt32(5));
                result.Add(adresse);
            }

            LocalData.Container.Adressen = result;
            reader.Close();
            con_delivery.Close();
        }

        public static void getKunden()
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM kunden", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<LocalData.Kunde> result = new List<LocalData.Kunde>();

            LocalData.Kunde kunde;

            while (reader.Read())
            {
                kunde = new LocalData.Kunde();
                kunde.Kundennummer = reader.GetInt32(0);
                kunde.Vorname = reader.GetString(1);
                kunde.Nachname = reader.GetString(2);
                kunde.addresse = LocalData.Container.Adressen.Single(adresse => adresse.Adressnummer == reader.GetInt32(3));
                result.Add(kunde);
            }

            LocalData.Container.Kunden = result;
            reader.Close();
            con_delivery.Close();
        }

        public static void getRouten()
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM routen", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<LocalData.Route> result = new List<LocalData.Route>();

            LocalData.Route route;

            while (reader.Read())
            {
                route = new LocalData.Route();
                route.RoutenID = reader.GetInt32(0);
                route.Name = reader.GetString(1);
                result.Add(route);
            }

            LocalData.Container.Routen = result;
        }
    }
}
