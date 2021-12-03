using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Holiberry.Api.Common.Response;
using Holiberry.Api.ServerLogs;
using Holiberry.Api.Models.Exceptions;
using Holiberry.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;

namespace Holiberry.Api.Attributes
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly IServerLogger _serverLogger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ApiExceptionFilterAttribute(IModelMetadataProvider modelMetadataProvider,
            IServerLogger serverLogger,
            IWebHostEnvironment hostingEnvironment)
        {
            _modelMetadataProvider = modelMetadataProvider;
            _hostingEnvironment = hostingEnvironment;
            _serverLogger = serverLogger;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {

            switch (context.Exception)
            {
                case UnauthorizedAccessException ex:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.HttpContext.Response.WriteJsonAsync(GetApiErrorResponse(context), "application/problem+json");
                    context.ExceptionHandled = true;
                    break;

                case ForbiddenAccessException ex:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    await context.HttpContext.Response.WriteJsonAsync(GetApiErrorResponse(context), "application/problem+json");
                    await LogLoggableException(ex);
                    context.ExceptionHandled = true;
                    break;

                case ServiceException ex:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.HttpContext.Response.WriteJsonAsync(GetApiErrorResponse(context), "application/problem+json");
                    await LogLoggableException(ex);
                    context.ExceptionHandled = true;
                    break;

                case NotFoundException ex:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.HttpContext.Response.WriteJsonAsync(GetApiErrorResponse(context), "application/problem+json");
                    await LogLoggableException(ex);
                    context.ExceptionHandled = true;
                    break;

                case Exception ex:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.HttpContext.Response.WriteJsonAsync(GetApiErrorResponse(context), "application/problem+json");
                    await LogException(ex);
                    context.ExceptionHandled = true;
                    break;
            }

            base.OnException(context);
        }

        private APIErrorResponse GetApiErrorResponse(ExceptionContext context)
        {
            var errors = new List<APIError>();
            if (context.Exception?.Data != null && context.Exception.Data.Count > 0)
            {
                foreach (DictionaryEntry d in context.Exception.Data)
                {
                    errors.Add(new APIError(d.Key?.ToString(), d.Value?.ToString() ?? "null"));
                }
            }

            var response = new APIErrorResponse()
            {
                Message = context.Exception.Message,
                StatusCode = context.HttpContext.Response.StatusCode,
                Errors = errors
            };

            return response;
        }

        private async Task LogException(Exception ex)
        {
            if (_hostingEnvironment.IsDevelopment())
                return;

            await _serverLogger.LogException(ex);
        }

        private async Task LogLoggableException<T>(T ex) where T : Exception
        {
            if (_hostingEnvironment.IsDevelopment())
                return;

            if (ex is ILoggableException<T>)
            {
                var exception = ex as ILoggableException<T>;

                if (exception.IsLoggingException)
                {
                    await _serverLogger.LogException(exception);
                }
            }
        }

    }
}
