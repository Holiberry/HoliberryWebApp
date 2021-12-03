using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Holiberry.Api.Common.Response;
using Holiberry.Api.ServerLogs;
using Holiberry.Api.ServerLogs;
using Holiberry.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Holiberry.Api.Middlewares
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ApiExceptionMiddleware(RequestDelegate next,
            IHttpContextAccessor contextAccessor,
            IWebHostEnvironment hostingEnvironment)
        {
            _next = next;
            _contextAccessor = contextAccessor;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await LogException(ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var errors = new List<APIError>();
            if (ex?.Data != null && ex.Data.Count > 0)
            {
                foreach (DictionaryEntry d in ex.Data)
                {
                    errors.Add(new APIError(d.Key?.ToString(), d.Value?.ToString() ?? "null"));
                }
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new APIErrorResponse()
            {
                Message = ex.Message,
                StatusCode = context.Response.StatusCode,
                Errors = errors
            };

            await context.Response.WriteJsonAsync(response, "application/problem+json");
        }

        private async Task LogException(Exception ex)
        {
            if (_hostingEnvironment.IsDevelopment())
                return;

            var serverLogger = (ServerLogger)_contextAccessor.HttpContext.RequestServices.GetService(typeof(IServerLogger));
            await serverLogger.LogException(ex);
        }
    }
}
