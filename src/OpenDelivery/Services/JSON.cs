using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace OpenDelivery.Services
{
    internal static class JSON
    {
        public static string ProduktListToJson(List<LocalData.BestelltesProdukt> produkte)
        {
            return JsonSerializer.Serialize(produkte);
        }


        public static List<LocalData.BestelltesProdukt> JsonToProduktList(string jsonstr)
        {
            return JsonSerializer.Deserialize<List<LocalData.BestelltesProdukt>>(jsonstr);
        }
    }
}
