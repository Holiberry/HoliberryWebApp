using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Services.AirQuality.Responses
{
    public class GetSensorQualityIndexResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("stCalcDate")]
        public string StCalcDate { get; set; }

        [JsonProperty("stIndexLevel")]
        public StIndexLevel StIndexLevel { get; set; }

        [JsonProperty("stSourceDataDate")]
        public string StSourceDataDate { get; set; }

        [JsonProperty("so2CalcDate")]
        public string So2CalcDate { get; set; }

        [JsonProperty("so2IndexLevel")]
        public So2IndexLevel So2IndexLevel { get; set; }

        [JsonProperty("so2SourceDataDate")]
        public string So2SourceDataDate { get; set; }

        [JsonProperty("no2CalcDate")]
        public string No2CalcDate { get; set; }

        [JsonProperty("no2IndexLevel")]
        public No2IndexLevel No2IndexLevel { get; set; }

        [JsonProperty("no2SourceDataDate")]
        public string No2SourceDataDate { get; set; }

        [JsonProperty("pm10CalcDate")]
        public string Pm10CalcDate { get; set; }

        [JsonProperty("pm10IndexLevel")]
        public Pm10IndexLevel Pm10IndexLevel { get; set; }

        [JsonProperty("pm10SourceDataDate")]
        public string Pm10SourceDataDate { get; set; }

        [JsonProperty("pm25CalcDate")]
        public string Pm25CalcDate { get; set; }

        [JsonProperty("pm25IndexLevel")]
        public Pm25IndexLevel Pm25IndexLevel { get; set; }

        [JsonProperty("pm25SourceDataDate")]
        public string Pm25SourceDataDate { get; set; }

        [JsonProperty("o3CalcDate")]
        public string O3CalcDate { get; set; }

        [JsonProperty("o3IndexLevel")]
        public O3IndexLevel O3IndexLevel { get; set; }

        [JsonProperty("o3SourceDataDate")]
        public string O3SourceDataDate { get; set; }

        [JsonProperty("stIndexStatus")]
        public bool StIndexStatus { get; set; }

        [JsonProperty("stIndexCrParam")]
        public string StIndexCrParam { get; set; }
    }


    public class StIndexLevel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("indexLevelName")]
        public string IndexLevelName { get; set; }
    }

    public class So2IndexLevel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("indexLevelName")]
        public string IndexLevelName { get; set; }
    }

    public class No2IndexLevel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("indexLevelName")]
        public string IndexLevelName { get; set; }
    }

    public class Pm10IndexLevel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("indexLevelName")]
        public string IndexLevelName { get; set; }
    }

    public class Pm25IndexLevel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("indexLevelName")]
        public string IndexLevelName { get; set; }
    }

    public class O3IndexLevel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("indexLevelName")]
        public string IndexLevelName { get; set; }
    }
}
