using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RealEstate.Core.Application.Dtos.Account;
using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Domain.Settings;
using RealEstate.Infrastructure.Identity.Context;
using RealEstate.Infrastructure.Identity.Entities;
using RealEstate.Infrastructure.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Identity
{
    //Main reason for creating this class is to follow the Single responsability
    public static class ServiceRegistration
    {
        // Extension methods | "Decorator"
        // This allows us to extend and create new functionallity following "Open-Closed Principle"
        public static void AddIdentityInfrastructure(this IServiceCollection service, IConfiguration config)
        {
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                service.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                service.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("IdentityConnection"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            }

            #region 'Identity'
            service.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            service.ConfigureApplicationCookie(opts =>
            {
                opts.LoginPath = "/User";
                opts.AccessDeniedPath = "/User/AccessDenied";
            });

            service.Configure<JWTSettings>(config.GetSection("JWTSettings"));

            service.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = config["JWTSettings:Issuer"],
                    ValidAudience = config["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"]))
                };
                opt.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = e =>
                    {
                        e.NoResult();
                        e.Response.StatusCode = 500;
                        e.Response.ContentType = "text/plain";
                        return e.Response.WriteAsync(e.Exception.ToString());
                    },
                    OnChallenge = e =>
                    {
                        e.HandleResponse();
                        e.Response.StatusCode = 401;
                        e.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You're not authorized"});
                        return e.Response.WriteAsync(result);
                    },
                    OnForbidden = e =>
                    {
                        e.Response.StatusCode = 403;
                        e.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You're not authorized to access this resource" });
                        return e.Response.WriteAsync(result);
                    }
                };
            });
            #endregion

            #region 'Services'
            service.AddTransient<IAccountService, AccountService>();
            #endregion
        }
    }
}
