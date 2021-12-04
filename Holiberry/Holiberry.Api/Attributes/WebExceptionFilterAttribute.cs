using System;
using System.Collections;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Holiberry.Api.Config;
using Holiberry.Api.Models.Exceptions;
using Holiberry.Api.Models.ServerLogs.Enums;
using Holiberry.Api.Models.Users.Statics;
using Holiberry.Api.ServerLogs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;

namespace Holiberry.Api.Attributes
{
    public class WebExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IServerLogger _serverLogger;

        public WebExceptionFilterAttribute(IModelMetadataProvider modelMetadataProvider, IWebHostEnvironment hostingEnvironment, IServerLogger serverLogger)
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
                    context.Result = WebExceptionResultView(context);
                    context.ExceptionHandled = true;
                    break;

                case ForbiddenAccessException ex:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Result = WebExceptionResultView(context);
                    await LogLoggableException(ex);
                    context.ExceptionHandled = true;
                    break;

                case ServiceException ex:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Result = WebExceptionResultView(context);
                    await LogLoggableException(ex);
                    context.ExceptionHandled = true;
                    break;

                case NotFoundException ex:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Result = WebExceptionResultView(context);
                    await LogLoggableException(ex);
                    context.ExceptionHandled = true;
                    break;

                case Exception ex:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Result = WebExceptionResultView(context);
                    await LogException(ex);
                    context.ExceptionHandled = true;
                    break;
            }

            base.OnException(context);
        }


        private IActionResult WebExceptionResultView(ExceptionContext context)
        {
            var returnUrl = context.HttpContext.Request.GetEncodedUrl();
            var message = context.Exception.Message;
            var layout = GetLayout(context);
            var exceptionTitle = GetExceptionTitle(context);

            var result = new ViewResult()
            {
                ViewName = "~/Views/Error/ServiceErrorView.cshtml",
                ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
            };
            result.ViewData.Add("ReturnUrl", returnUrl);
            result.ViewData.Add("ErrorMessage", message);
            result.ViewData.Add("Layout", layout);
            result.ViewData.Add("ExceptionTitle", exceptionTitle);

            if (_hostingEnvironment.IsDevelopment() || context.HttpContext.User.IsInRole(UserRoles.Dev))
            {
                result.ViewData.Add("AdditionalExceptionInfo", GetAdditionalExceptionInfo(context));
            }

            return result;
        }


        private string GetLayout(ExceptionContext context)
        {
            ControllerActionDescriptor controllerActionDescriptor = null;
            if (context.ActionDescriptor is ControllerActionDescriptor)
                controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            string layout = LayoutsConfig.Clear;
            if (controllerActionDescriptor != null)
            {
                string actionName = controllerActionDescriptor.ActionName;
                string controllerName = controllerActionDescriptor.ControllerName;

                if (actionName.Contains("Partial"))
                {
                    layout = LayoutsConfig.Clear;
                }
                else if (controllerName.StartsWith("A_"))
                {
                    layout = LayoutsConfig.Admin;
                }
            }

            return layout;
        }

        private string GetExceptionTitle(ExceptionContext context)
        {
            string title = "Wystąpił błąd";
            switch (context.HttpContext.Response.StatusCode)
            {
                case 400:
                    title = "Wystąpił błąd przetwarzania zapytania na serwerze";
                    break;
                case 401:
                    title = "Nieautoryzowany dostęp do zasobów";
                    break;
                case 403:
                    title = "Brak uprawnień do przeglądania zasobów";
                    break;
                case 404:
                    title = "Nie znaleziono poszukiwanych zasobów";
                    break;
                case 500:
                    title = "Wystąpił nieoczekiwany błąd";
                    break;
            }

            return title;
        }

        private string GetAdditionalExceptionInfo(ExceptionContext context)
        {
            var exceptionData = new StringBuilder();
            if (context.Exception.Data != null && context.Exception.Data.Count > 0)
            {
                exceptionData.Append("<ul>\n");
                foreach (DictionaryEntry d in context.Exception.Data)
                {
                    exceptionData.Append($"<li><b>Key:</b> {d.Key.ToString()} <b class='ml-100'>Value:</b> {d.Value}</li>\n");
                }
                exceptionData.Append("</ul>\n");
            }

            return $"<p><b>Message:</b> {context.Exception?.Message}</p>\n" +
                $"<p><b>Action:</b> {((ControllerActionDescriptor)context?.ActionDescriptor)?.ActionName}</p>\n" +
                $"<p><b>Controller:</b> {((ControllerActionDescriptor)context?.ActionDescriptor)?.ControllerName}</p>\n" +
                $"<p><b>Path:</b> {context.HttpContext?.Request?.Path.Value}</p>\n" +
                $"<p><b>QueryString:</b> {context.HttpContext?.Request?.QueryString}</p>\n" +
                $"<p><b>ExceptionData:</b> {exceptionData?.ToString()}</p>\n" +
                $"<p><b>Source:</b> {context.Exception?.Source}</p>\n" +
                $"<p><b>StackTrace:</b> {context.Exception?.ToString()}</p>\n" +
                $"<p><b>InnerException:</b> {context.Exception?.InnerException?.ToString()}</p>\n";
        }

        private async Task LogException(Exception ex)
        {
            if (_hostingEnvironment.IsDevelopment())
                return;

            await _serverLogger.LogCustomException(ServerLogLevelE.Critical, ex.Source, ex?.Message, ex.StackTrace, string.Format("{0}---{1}", ex.InnerException?.Message, ex?.InnerException?.StackTrace));
        }

        private async Task LogLoggableException<T>(T ex) where T : Exception
        {
            //if (_hostingEnvironment.IsDevelopment())
            //    return;

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
