using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Hosting;
using Holiberry.Api.Managers.ViewRender;
using Holiberry.Api.ServerLogs;
using Holiberry.Api.Models.ServerLogs.Enums;
using Holiberry.Api.Models.Users.Statics;

namespace Holiberry.Api.Middlewares
{
    public class WebExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public WebExceptionMiddleware(RequestDelegate next, IWebHostEnvironment hostingEnvironment)
        {
            _next = next;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task Invoke(HttpContext context, IServerLogger serverLogger, IRazorViewRenderer razorViewRenderer)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await LogException(exception, serverLogger);
                await HandleExceptionAsync(context, exception, razorViewRenderer);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, IRazorViewRenderer razorViewRenderer)
        {
            var viewData = new Dictionary<string, object>()
            {
                { "ExceptionTitle", "Wystąpił nieoczekiwany błąd" },
                { "ReturnUrl", context.Request.GetEncodedUrl() },
                { "ErrorMessage", exception.Message },
            };
            if (_hostingEnvironment.IsDevelopment() || context.User.IsInRole(UserRoles.Dev))
            {
                viewData.Add("AdditionalExceptionInfo", GetAdditionalExceptionInfo(context, exception));
            }

            var view = await razorViewRenderer.RenderViewToStringAsync("~/Views/Error/ServiceErrorView.cshtml", viewData);


            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(view);
        }

        private string GetAdditionalExceptionInfo(HttpContext context, Exception ex)
        {
            var exceptionData = new StringBuilder();
            if (ex?.Data != null && ex.Data.Count > 0)
            {
                exceptionData.Append("<ul>\n");
                foreach (DictionaryEntry d in ex.Data)
                {
                    exceptionData.Append($"<li><b>Key:</b> {d.Key.ToString()} <b class='ml-100'>Value:</b> {d.Value}</li>\n");
                }
                exceptionData.Append("</ul>\n");
            }

            return $"<p><b>Message:</b> {ex?.Message}</p>\n" +
                $"<p><b>Path:</b> {context?.Request?.Path.Value}</p>\n" +
                $"<p><b>QueryString:</b> {context?.Request?.QueryString}</p>\n" +
                $"<p><b>ExceptionData:</b> {exceptionData?.ToString()}</p>\n" +
                $"<p><b>Source:</b> {ex?.Source}</p>\n" +
                $"<p><b>StackTrace:</b> {ex?.ToString()}</p>\n" +
                $"<p><b>InnerException:</b> {ex?.InnerException?.ToString()}</p>\n";
        }

        private async Task LogException(Exception exception, IServerLogger logger)
        {
            if (_hostingEnvironment.IsDevelopment())
                return;

            await logger.LogCustomException(ServerLogLevelE.Critical, exception.Source, exception?.Message, exception.StackTrace, string.Format("{0}---{1}", exception.InnerException?.Message, exception?.InnerException?.StackTrace));
        }
    }
}
