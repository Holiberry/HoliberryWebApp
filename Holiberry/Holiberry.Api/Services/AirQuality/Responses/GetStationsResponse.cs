using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Services.AirQuality.Responses
{
    public class GetStationsResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("stationName")]
        public string StationName { get; set; }

        [JsonProperty("gegrLat")]
        public string GegrLat { get; set; }

        [JsonProperty("gegrLon")]
        public string GegrLon { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }

        [JsonProperty("addressStreet")]
        public string AddressStreet { get; set; }
    }


    public class Commune
    {
        [JsonProperty("communeName")]
        public string CommuneName { get; set; }

        [JsonProperty("districtName")]
        public string DistrictName { get; set; }

        [JsonProperty("provinceName")]
        public string ProvinceName { get; set; }
    }

    public class City
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("commune")]
        public Commune Commune { get; set; }
    }
}
