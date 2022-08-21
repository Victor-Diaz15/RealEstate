using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RealEstateAPI.WebApi.Extensions
{
    public static class ServiceExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection svc)
        {
            svc.AddSwaggerGen(opt =>
            {
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => opt.IncludeXmlComments(xmlFile));

                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RealEstate API",
                    Contact = new OpenApiContact
                    {
                        Name = "Dav M.M. y los BDM (banco davinci montas)"
                    }
                });

                opt.DescribeAllParametersInCamelCase();
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your bearer token in this format - Bearer {your token goes in here}"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id ="Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        }, new List<string>()
                    },
                });
            });
        }

        public static void AddApiVersioningExtension(this IServiceCollection svc)
        {
            svc.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

        }

    }
}
