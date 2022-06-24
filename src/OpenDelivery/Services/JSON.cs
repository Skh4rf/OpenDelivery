using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace OpenDelivery.Services
{
    internal static class JSON
    {
        public static string ProduktListToJson(List<LocalData.BestelltesProdukt> produkte)
        {
            string jsonstr = JsonSerializer.Serialize(produkte);
            List<char> jsonlist = jsonstr.ToCharArray().ToList();

            string newjsonstr = "\'";

            foreach (char c in jsonlist)
            {
                if (c.Equals('\"'))
                {
                    newjsonstr += '\\';
                }
                newjsonstr += c;
            }

            return newjsonstr + "\'";
        }

        public static List<LocalData.BestelltesProdukt> JsonToProduktList(string jsonstr)
        {
            return JsonSerializer.Deserialize<List<LocalData.BestelltesProdukt>>(jsonstr);
        }
    }
}
