using Holiberry.Api.Attributes;
using Holiberry.Api.Config;
using Holiberry.Api.Services.AirQuality;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Public.Controllers
{

    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    [Area(AreasConfig.Public)]
    [ApiAuthorize]
    [Route("v1/public/air-quality")]
    public class P_AirQualityController : Controller
    {
        private readonly IAirQualityApi _airQualityApi;


        public P_AirQualityController(IAirQualityApi airQualityApi)
        {
            _airQualityApi = airQualityApi;
        }


        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAirQuality(long? cityId)
        {
            if (cityId == null)
                cityId = 1064;

            var stations = await _airQualityApi.GetStations();

            var cityStations = stations
                .Where(a => a.City.Id == cityId) // WRO
                .Select(a => new StationDTO
                {
                    StationId = a.Id,
                    Lat = double.TryParse(a.GegrLat, NumberStyles.Any, new CultureInfo("en"), out var lat) ? lat : (double?)null,
                    Lng = double.TryParse(a.GegrLon, NumberStyles.Any, new CultureInfo("en"), out var lng) ? lng : (double?)null,
                    StationName = a.StationName,
                })
                .ToList();

            foreach (var station in cityStations)
            {
                var airQualityIndex = await _airQualityApi.GetSensorAirQualityIndex(station.StationId);


                station.AvgIndex = airQualityIndex?.StIndexLevel?.IndexLevelName;

                station.SO2 = airQualityIndex?.So2IndexLevel?.IndexLevelName;
                station.NO2 = airQualityIndex?.No2IndexLevel?.IndexLevelName;
                station.PM10 = airQualityIndex?.Pm10IndexLevel?.IndexLevelName;
                station.PM25 = airQualityIndex?.Pm25IndexLevel?.IndexLevelName;
                station.O3 = airQualityIndex?.O3IndexLevel?.IndexLevelName;


                var sensors = await _airQualityApi.GetSensors(station.StationId);
                foreach (var sensor in sensors)
                {
                    var readings = await _airQualityApi.GetSensorReadings(sensor.Id);

                    switch (readings.Key)
                    {
                        case "O3":
                            station.O3Readings = readings.Values?.Select(a => new ReadingValue() { Value = a.value, Date = a.Date })?.ToList();
                            break;

                        case "NO2":
                            station.NO2Readings = readings.Values?.Select(a => new ReadingValue() { Value = a.value, Date = a.Date })?.ToList();
                            break;

                        case "SO2":
                            station.SO2Readings = readings.Values?.Select(a => new ReadingValue() { Value = a.value, Date = a.Date })?.ToList();
                            break;

                        case "PM10":
                            station.PM10Readings = readings.Values?.Select(a => new ReadingValue() { Value = a.value, Date = a.Date })?.ToList();
                            break;

                        case "PM2.5":
                            station.PM25Readings = readings.Values?.Select(a => new ReadingValue() { Value = a.value, Date = a.Date })?.ToList();
                            break;

                        case "CO":
                            station.COReadings = readings.Values?.Select(a => new ReadingValue() { Value = a.value, Date = a.Date })?.ToList();
                            break;

                        default:
                            break;
                    }
                }
            }

            return Ok(cityStations);
        }


    }


    public class StationDTO
    {
        public int StationId { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string StationName { get; set; }


        public string AvgIndex { get; set; }

        public string PM10 { get; set; }
        public List<ReadingValue> PM10Readings { get; set; }
        public string PM25 { get; set; }
        public List<ReadingValue> PM25Readings { get; set; }
        public string SO2 { get; set; }
        public List<ReadingValue> SO2Readings { get; set; }
        public string NO2 { get; set; }
        public List<ReadingValue> NO2Readings { get; set; }
        public string O3 { get; set; }
        public List<ReadingValue> O3Readings { get; set; }


        public List<ReadingValue> COReadings { get; set; }
    }

    public class ReadingValue
    {
        public double? Value { get; set; }
        public string Date { get; set; }
    }
}
