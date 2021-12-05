using Holiberry.Api.Services.AirQuality.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Services.AirQuality
{
    public interface IAirQualityApi
    {
        Task<List<GetStationsResponse>> GetStations();
        Task<List<GetSensorsResponse>> GetSensors(int stationId);
        Task<GetSensorQualityIndexResponse> GetSensorAirQualityIndex(int sensorId);
        Task<GetSensorReadingsResponse> GetSensorReadings(int sensorId);
    }


    public class AirQualityApi : IAirQualityApi
    {
        private readonly IAirQualityClient _client;

        public AirQualityApi(IAirQualityClient client)
        {
            _client = client;
        }


        public async Task<List<GetStationsResponse>> GetStations()
        {
            var response = await _client.GetAsync<List<GetStationsResponse>>("station/findAll");

            return response;
        }


        public async Task<List<GetSensorsResponse>> GetSensors(int stationId)
        {
            var response = await _client.GetAsync<List<GetSensorsResponse>>($"station/sensors/{stationId}");

            return response;
        }


        public async Task<GetSensorQualityIndexResponse> GetSensorAirQualityIndex(int sensorId)
        {
            var response = await _client.GetAsync<GetSensorQualityIndexResponse>($"aqindex/getIndex/{sensorId}");

            return response;
        }

        public async Task<GetSensorReadingsResponse> GetSensorReadings(int sensorId)
        {
            var response = await _client.GetAsync<GetSensorReadingsResponse>($"data/getData/{sensorId}");

            return response;
        }
    }
}
