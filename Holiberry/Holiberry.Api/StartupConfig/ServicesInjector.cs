using Holiberry.Api.Managers.File;
using Holiberry.Api.Managers.ViewRender;
using Holiberry.Api.ServerLogs;
using Holiberry.Api.Managers.File;
using Holiberry.Api.Managers.ViewRender;
using Holiberry.Api.Persistence;
using Holiberry.Api.ServerLogs;
using Microsoft.Extensions.DependencyInjection;

namespace Holiberry.Api.StartupConfig
{
    public static class ServicesInjector
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IRazorViewRenderer, RazorViewRenderer>();
            services.AddScoped<IServerLogger, ServerLogger>();
            
            services.AddScoped<IDbFileService, DbFileService>();
        }
    }

}