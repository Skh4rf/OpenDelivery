using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenDelivery.Services
{
    internal static class Database
    {
        private static MySqlConnection con_delivery = new MySqlConnection(@"server=localhost;userid=root;password=;database=opendelivery");

        public static void refreshData()
        {
            getKoordinaten();
            getAdressen();
            getKunden();
            getRouten();
            getProdukte();
            getBestellungen();
        }

        #region SQLgetData

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
                adresse.Ort = reader.GetString(2);
                adresse.Strasse = reader.GetString(3);
                adresse.Nummer = reader.GetInt32(4);
                try
                {
                    adresse.Adresszusatz = reader.GetString(5);
                }
                catch (Exception) { adresse.Adresszusatz = null; }
                adresse.koordinate = LocalData.Container.Koordinaten.Single(koordinate => koordinate.KoordinatenID == reader.GetInt32(6));
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
                kunde.adresse = LocalData.Container.Adressen.Single(adresse => adresse.Adressnummer == reader.GetInt32(3));
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
            reader.Close();
            con_delivery.Close();
        }

        public static void getProdukte()
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Produkte", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<LocalData.Produkt> result = new List<LocalData.Produkt>();

            LocalData.Produkt produkt;

            while (reader.Read())
            {
                produkt = new LocalData.Produkt();
                produkt.Artikelnummer = reader.GetInt32(0);
                produkt.Name = reader.GetString(1);
                produkt.Einheit = reader.GetString(2);
                result.Add(produkt);
            }

            LocalData.Container.produkte = result;

            con_delivery.Close();
            reader.Close();
        }

        public static void getBestellungen()
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Bestellungen", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            List<LocalData.Bestellung> result = new List<LocalData.Bestellung>();

            LocalData.Bestellung bestellung;

            while (reader.Read())
            {
                bestellung = new LocalData.Bestellung();
                bestellung.Bestellnummer = reader.GetInt32(0);
                bestellung.kunde = LocalData.Container.Kunden.Single(kunde => kunde.Kundennummer == reader.GetInt32(1));
                bestellung.route = LocalData.Container.Routen.Single(route => route.RoutenID == reader.GetInt32(2));
                bestellung.Produkte = JSON.JsonToProduktList(reader.GetString(3));
                result.Add(bestellung);
            }

            LocalData.Container.Bestellungen = result;

            con_delivery.Close();
            reader.Close();
        }

        #endregion SQLgetData

        #region SQLupdateValues
        public static void updateValue(string tablename, string valuenumbername, string valuenumber, string valuename, string value)
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"UPDATE {tablename} SET {tablename}.{valuename} = {value} WHERE {tablename}.{valuenumbername} = {valuenumber};", con_delivery);
            cmd.ExecuteNonQuery();
            con_delivery.Close();
        }
        #endregion SQLupdateValues

        #region SQLcreateTable
        public static LocalData.Koordinate createTable(LocalData.Koordinate koordinate)
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"CALL createKoordinate({koordinate.Latitude}, {koordinate.Longitude})", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                koordinate.KoordinatenID = reader.GetInt32(0);
            }

            reader.Close();
            con_delivery.Close();

            LocalData.Container.Koordinaten.Add(koordinate);
            return koordinate;
        }

        public static LocalData.Adresse createTable(LocalData.Adresse adresse)
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"CALL createAdresse({adresse.Postleitzahl}, {toSQLString(adresse.Ort)}, {toSQLString(adresse.Strasse)}, {adresse.Nummer}, {toSQLString(adresse.Adresszusatz)}, {adresse.koordinate.KoordinatenID})", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                adresse.Adressnummer = reader.GetInt32(0);
            }

            reader.Close();
            con_delivery.Close();

            LocalData.Container.Adressen.Add(adresse);
            return adresse;
        }

        public static LocalData.Kunde createTable(LocalData.Kunde kunde)
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"CALL createKunde({toSQLString(kunde.Vorname)}, {toSQLString(kunde.Nachname)}, {kunde.adresse.Adressnummer})", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                kunde.Kundennummer = reader.GetInt32(0);
            }

            reader.Close();
            con_delivery.Close();

            LocalData.Container.Kunden.Add(kunde);
            return kunde;
        }

        public static LocalData.Route createTable(LocalData.Route route)
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"CALL createRoute({toSQLString(route.Name)})", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                route.RoutenID = reader.GetInt32(0);
            }

            reader.Close();
            con_delivery.Close();

            LocalData.Container.Routen.Add(route);
            return route;
        }

        public static LocalData.Produkt createTable(LocalData.Produkt produkt)
        {
            con_delivery.Open();
            MySqlCommand cmd = new MySqlCommand($"CALL createProdukt({toSQLString(produkt.Name)}, {toSQLString(produkt.Einheit)})", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                produkt.Artikelnummer = reader.GetInt32(0);
            }

            reader.Close();
            con_delivery.Close();

            LocalData.Container.produkte.Add(produkt);
            return produkt;
        }

        public static LocalData.Bestellung createTable(LocalData.Bestellung bestellung)
        {
            con_delivery.Open();

            MySqlCommand cmd = new MySqlCommand($"CALL createBestellung({bestellung.kunde.Kundennummer}, {bestellung.route.RoutenID}, {JSON.ProduktListToJson(bestellung.Produkte)})", con_delivery);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                bestellung.Bestellnummer = reader.GetInt32(0);
            }

            reader.Close();
            con_delivery.Close();

            LocalData.Container.Bestellungen.Add(bestellung);
            return bestellung;
        }
        #endregion SQLcreateTable

        private static string toSQLString(string str)
        {
            return "\"" + str + "\"";
        }
    }
}
