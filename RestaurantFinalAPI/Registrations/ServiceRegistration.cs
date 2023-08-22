using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Infrastructure.TokenServıces;
using RestaurantFinalAPI.Configurations.ReadAppsetting;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Serilog.Core;
using Microsoft.AspNetCore.HttpLogging;
using RestaurantFinalAPI.Middlewares;
using Microsoft.OpenApi.Models;

namespace RestaurantFinalAPI.Registrations
{
    public static class ServiceRegistration
    {
        public static void AddPresentationService(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer("Admin", options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,//tokunumuzu kim/hansi origin islede biler
                    ValidateIssuer = true, //tokunu kim palylayir
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true, //tokenin ozel keyi

                    ValidAudience = Configuration.GetTokenAudience,

                    ValidIssuer = Configuration.GetTokenIssure,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetTokenSecurityKey)),

                    //tiken omru qeder islemesi ucun
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType= ClaimTypes.Role,
                };
            });

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 3.1 Web API"
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });

            //Cross policy
            services.AddCors(option => option.AddDefaultPolicy(policy => policy
                .WithOrigins("https://localhost:7085//", "http://localhost:7085/")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()));

            //bunun asagida midilwarede yazmisam(app.Use - bu Proqram.cs da) bu da requestlerin logu ucun lazim
            services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add("sec-ch-ua");
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });

            services.AddHttpContextAccessor(); //context gormek ucun http
            //services.AddScoped<GlobalExceptionHandlerMiddleware>();
        }
    }
}
