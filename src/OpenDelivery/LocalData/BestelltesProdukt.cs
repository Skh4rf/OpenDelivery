namespace OpenDelivery.LocalData
{
    internal class BestelltesProdukt : Produkt
    {
        public double Menge { get; set; }

        public BestelltesProdukt() : base() { }

        public override string ToString()
        {
            return $"{Menge} {Einheit} {Name}";
        }
    }
}
