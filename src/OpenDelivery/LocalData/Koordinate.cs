namespace OpenDelivery.LocalData
{
    internal class Koordinate
    {
        public int KoordinatenID { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public Koordinate() { }

        public Koordinate(float latitude, float longitude) { Latitude = latitude; Longitude = longitude; }
    }
}
