using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServiceDoklado
{
    public static class JsonSerializationConfig
    {
        public static void ConfigureGlobalSettings()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc, // Enforce UTC
                DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ",  // Enforce required format
                Formatting = Formatting.Indented,              // Optional: Pretty-printing for debugging
                Converters = new List<JsonConverter>           // Optional: Add any custom converters
            {
                new Newtonsoft.Json.Converters.StringEnumConverter() // Example: Enum as strings
            }
            };
        }
    }

}
