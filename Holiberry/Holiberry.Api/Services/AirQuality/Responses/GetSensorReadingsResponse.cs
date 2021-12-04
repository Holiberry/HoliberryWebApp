using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Services.AirQuality.Responses
{
    public class GetSensorReadingsResponse
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("values")]
        public List<Value> Values { get; set; }
    }


    public class Value
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        
        [JsonProperty("value")]
        public double? value { get; set; }
    }
}
