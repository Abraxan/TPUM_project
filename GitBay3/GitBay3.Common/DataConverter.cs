using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Common
{
    public class DataConverter
    {

        public static T Deserialize<T>(string data)
        {
            var result = JsonConvert.DeserializeObject<T>(data);
            return result;
        }

        public static string Serialize(object t)
        {
            var result = JsonConvert.SerializeObject(t);
            return result;
        }

        public static List<Entry> Convert(Dictionary<Currencies, float> data)
        {
            var result = new List<Entry>();
            foreach(var k in data.Keys)
            {
                result.Add(new Entry()
                {
                    Currency = k,
                    Value = data[k]
                });
            }
            return result;
        }

        public static Dictionary<Currencies, float> Convert(List<Entry> data)
        {
            var result = new Dictionary<Currencies, float>();
            foreach (var e in data)
            {
                if (!result.Keys.Contains(e.Currency))
                {
                    result.Add(e.Currency, e.Value);
                }
            }
            return result;
        }
    }
}
