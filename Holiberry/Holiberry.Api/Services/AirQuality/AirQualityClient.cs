using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Holiberry.Api.Services.AirQuality
{
    public interface IAirQualityClient
    {
        Task<TResponse> GetAsync<TResponse>(string path) where TResponse : class;
    }


    public class AirQualityClient : HttpClient, IAirQualityClient
    {
        public AirQualityClient()
            : base()
        {
            BaseAddress = new Uri("https://api.gios.gov.pl/pjp-api/rest/");
        }


        public async Task<TResponse> GetAsync<TResponse>(string path) where TResponse : class
        {
            base.DefaultRequestHeaders.Add("ContentType", "application/json");
            base.DefaultRequestHeaders.Add("Accept", "application/json");


            var response = await base.GetAsync(path);

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TResponse>(content);

            return result;
        }
    }
}
