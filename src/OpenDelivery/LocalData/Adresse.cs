namespace OpenDelivery.LocalData
{
    internal class Adresse
    {
        public int Adressnummer { get; set; }
        public int Postleitzahl { get; set; }
        public string Ort { get; set; }
        public string Strasse { get; set; }
        public int Nummer { get; set; }
        public string Adresszusatz { get; set; }
        public Koordinate koordinate;

        public Adresse() { }

        public Adresse(int plz, string ort, string strasse, int nummer, string adresszusatz, Koordinate koord)
        {
            Postleitzahl = plz;
            Ort = ort;
            Strasse = strasse;
            Nummer = nummer;
            Adresszusatz = adresszusatz;
            koordinate = koord;
        }

        public string getCityString()
        {
            return Postleitzahl.ToString() + " " + Ort;
        }

        public string getStreetString()
        {
            return Strasse + " " + Nummer + " " + Adresszusatz;
        }
    }
}
