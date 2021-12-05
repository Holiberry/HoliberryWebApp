using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Services.AirQuality.Responses
{
    public class GetSensorsResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }


        [JsonProperty("stationId")]
        public int StationId { get; set; }


        [JsonProperty("param")]
        public Param Param { get; set; }
    }

    public class Param
    {
        [JsonProperty("paramName")]
        public string ParamName { get; set; }

        [JsonProperty("paramFormula")]
        public string ParamFormula { get; set; }

        [JsonProperty("paramCode")]
        public string ParamCode { get; set; }

        [JsonProperty("idParam")]
        public int IdParam { get; set; }
    }

}
