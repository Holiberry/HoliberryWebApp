using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holiberry.Api.Config;
using Holiberry.Api.Mappings;
using Holiberry.Api.ServerLogs.Config;
using Holiberry.Api.Models.Users.Entities.Identity;
using Holiberry.Api.Persistence;
using Holiberry.Api.Extensions;
using Holiberry.Api.StartupConfig;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Http;

namespace Holiberry.Api
{
    public class Startup
    {
        public IWebHostEnvironment _env { get; set; }
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //-----------------------------AUTH CONFIGS----------------------------//
            ConfigAuth.IssuerSigningKey = Configuration.GetValue<string>("ConfigAuth:IssuerSigningKey");
            ConfigAuth.ValidIssuer = Configuration.GetValue<string>("ConfigAuth:ValidIssuer");
            ConfigAuth.ValidAudience = Configuration.GetValue<string>("ConfigAuth:ValidAudience");
            //-----------------------------/AUTH CONFIGS----------------------------//


            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policyBuilder =>
                    {
                        policyBuilder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            // Add your configs object here
            services.AddOptions();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Database")));

            services.AddAuthentication().AddCookie(cfg =>
            {
                cfg.SlidingExpiration = true;
                //cfg.ExpireTimeSpan = TimeSpan.FromDays(7);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = ConfigAuth.ValidIssuer,
                        ValidAudience = ConfigAuth.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigAuth.IssuerSigningKey))
                    };
                });

            ////Configure identity
            services.AddIdentity<UserM, RoleM>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();



            //LowerCaseURLs
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            //Configure SecurityStamp object
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                //Configure how ofert securicy stamp should be validated
                options.ValidationInterval = TimeSpan.FromMinutes(30);
                options.OnRefreshingPrincipal = (context) =>
                {
                    return Task.CompletedTask;
                };
            });

            //Add application session
            services.AddSession(options =>
            {
                // Set session timeout
                options.IdleTimeout = TimeSpan.FromDays(1);
            });

            //Configure identityoptions
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });


            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                //options.Cookie.Expiration = TimeSpan.FromSeconds(5); //Old code
                options.LoginPath = "/account/login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/account/logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(10);
                // options.EventsType = typeof(CustomCookieAuthenticationEvents);
            });


            var builder = services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Holiberry"))))
                .AddNewtonsoftJson(options =>
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            if (_env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }

            services.AddHttpClient();

            //Dokumentacja API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Holiberry API",
                    Description = "",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                            new OpenApiSecurityScheme { Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "Bearer" } }
                            ,new string[] {}
                    }
                });
            });

            //Data protection (don't logout users after deploy)
            services.AddDataProtection()
                .SetApplicationName("holiberry")
                .PersistKeysToFileSystem(new DirectoryInfo(@"netcore-keys/"));


            //AutoMapper - look for profiles and atributes in given assembly
            services.AddAutoMapper(typeof(MappingProfile));

            services.InjectServices();

            // Config API
            ConfigAPI.APIUrl = Configuration.GetValue<string>("ConfigAPI:APIUrl");
            ConfigAPI.WebAppUrl = Configuration.GetValue<string>("ConfigAPI:WebAppUrl");

            // SERVERLOG CONFIGURATION
            ServerLogsConfig.RunSource = Configuration.GetValue<string>("ServerLogsConfig:RunSource");

        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseApiExceptionHandler();


            // jeœli chcemy mieæ informacje w HttpContext np. o adresie Ip requestu (w innym wypadku jest on gubiony na proxy serwera)
            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.All
            });

            //app.UseHttpsRedirection();

            if (env.IsProduction() || env.IsDevelopment())
            {
                //Dokumentacja API
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Holiberry");
                    c.DocExpansion(DocExpansion.None);
                });
            }

            // kolejnosc kolejnych jest wazna
            app.UseStaticFiles();

            app.UseCors("AllowAll");
            app.UseRouting();
            //app.UseCors();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    new { area = "Web", controller = "W_Home", action = "Index" });
            });
        }
    }
}