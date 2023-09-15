
using System.Text;
using API.Helpers;
using API.Helpers.Errors;
using API.Services;
using Aplicacion.UnitOfWork;
using AspNetCoreRateLimit;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()    //WithOrigins("https://domini.com")
                .AllowAnyMethod()           //WithMethods(*GET", "POST")
                .AllowAnyHeader());         //WithHeaders(*accept*, "content-type")
            });

        public static void AddAplicacionServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthorizationHandler, GlobalVerbRoleHandler>();
        }

        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuration from AppSettings
            services.Configure<JWT>(configuration.GetSection("JWT"));

            //Adding Athentication - JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                    };
                });
        }
        public static void AddValidationErrors(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {

                    var errors = actionContext.ModelState.Where(u => u.Value.Errors.Count > 0)
                                                    .SelectMany(u => u.Value.Errors)
                                                    .Select(u => u.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidation()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }

        public static void ConfigureRate(this IServiceCollection services){
            services.AddMemoryCache();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();
            services.Configure<IpRateLimitOptions>(opts =>{
                opts.EnableEndpointRateLimiting = true;
                opts.StackBlockedRequests = false;
                opts.HttpStatusCode = 429;
                opts.RealIpHeader = "X-Real-Ip";
                opts.GeneralRules = new(){
                    new(){
                        Endpoint = "*",
                        Period = "20s",
                        Limit = 4
                    }   
                };
            });
        }

        public static void ConfigureVer(this IServiceCollection services){
            services.AddApiVersioning(opts => {
                opts.DefaultApiVersion = new(1,0);
                opts.AssumeDefaultVersionWhenUnspecified = true;
                opts.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("ver"),
                    new HeaderApiVersionReader("X-Version")
                );
                opts.ReportApiVersions = true;
            });            
        }
    }

}