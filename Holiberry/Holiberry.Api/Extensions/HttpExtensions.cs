using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Holiberry.Api.Extensions
{
    public static class HttpExtensions
    {
        //public static readonly JsonSerializer serializer = new JsonSerializer
        //{
        //    NullValueHandling = NullValueHandling.Ignore
        //};

        public static async Task WriteJsonAsync<T>(this HttpResponse response, T obj, string contentType = null)
        {
            response.ContentType = contentType ?? "application/json";

            await response.Body.WriteAsync(JsonSerializer.SerializeToUtf8Bytes(obj, typeof(T), new JsonSerializerOptions()
            {
                IgnoreNullValues = true
            }));
        }


    }
}
