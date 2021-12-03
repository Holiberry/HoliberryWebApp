using Holiberry.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Holiberry.Api.Extensions
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiExceptionMiddleware>();
        }
    }
}
