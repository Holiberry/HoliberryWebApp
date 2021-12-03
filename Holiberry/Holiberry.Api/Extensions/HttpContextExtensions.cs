using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Holiberry.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task<(string path, string queryString, string rawRequestBody)> GetRequestDataAsync(this HttpContext context)
        {
            using var responseBodyStream = new MemoryStream();
            await context?.Request.Body.CopyToAsync(responseBodyStream);
            
            using var reader = new StreamReader(responseBodyStream);
            var rawBody = string.Empty;
            try
            {
                responseBodyStream.Position = 0;
                rawBody = await reader.ReadToEndAsync();

                if (rawBody.Contains("password") || rawBody.Contains("login"))
                    rawBody = null;
            }
            catch (Exception readEx)
            {
                rawBody = $"Read request body exception: {readEx.Message}";
            }

            var path = $"{context.Request.Method}  {context.Request.Path.ToString()}";
            var queryString = context.Request.QueryString.ToString();

            return (path, queryString, rawBody);
        }

    }
}