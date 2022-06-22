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
        protected static MySqlConnection con_delivery = new MySqlConnection(@"server=localhost;userid=root;password=;database=delivery");

        public static List<string> routeNames { get; set; } = new List<string>();
        public static List<string> customer { get; set; } = new List<string>();

        public static void refreshData()
        {
            routeNames.Clear();
            customer.Clear();
            getRoutes();
            getCustomerNames();
        }

        public static void getRoutes()
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT Tag FROM Lieferdaten", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string> result = new List<string>();

            while (reader.Read())
            {
                result.Add(reader.GetString(0));
            }

            con_delivery.Close();
            reader.Close();

            routeNames = result;
        }

        public static void getCustomerNames()
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT Vorname FROM Kunden", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string> result = new List<string>();

            int count = 0;

            while (reader.Read())
            {
                count++;
                result.Add(reader.GetString(0));
            }

            con_delivery.Close();
            reader.Close();

            customer = result;
        }
    }
}
