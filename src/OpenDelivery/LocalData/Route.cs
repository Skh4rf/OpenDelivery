namespace OpenDelivery.LocalData
{
    internal class Route
    {
        public int RoutenID { get; set; }
        public string Name { get; set; }

        public Route() { }

        public Route(string name) { Name = name; }
    }
}
