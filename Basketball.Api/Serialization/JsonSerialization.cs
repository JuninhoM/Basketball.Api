using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basketball.Api.Serialization
{
    public sealed class JsonSerializer
    {
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        };

        private static readonly JsonSerializerSettings _camelCaseSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static string SerializeObject(object o, bool camelCaseReturn = false)
        {
            if (!camelCaseReturn)
                return JsonConvert.SerializeObject(o, _settings);
            return JsonConvert.SerializeObject(o, _camelCaseSettings);
        }
    }
}
