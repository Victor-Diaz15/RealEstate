using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Core.Application.Interfaces.Repositories;
using RealEstate.Infrastructure.Persistence.Context;
using RealEstate.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrastructure.Persistence
{
   //Main reason for creating this class is to follow the Single responsability
    public static class ServiceRegistration
    {
        // Extension methods | "Decorator"
        // This allows us to extend and create new functionallity following "Open-Closed Principle"
        public static void AddPersistanceInfrastructure(this IServiceCollection service, IConfiguration config)
        {
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                service.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                service.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    m => m.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
            }

            #region 'repositories'

            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddTransient<IImprovementRepository, ImprovementRepository>();
            service.AddTransient<ISaleTypeRepository, SaleTypeRepository>();
            service.AddTransient<IPropertyTypeRepository, PropertyTypeRepository>();
            service.AddTransient<IPropertyRepository, PropertyRepository>();
            service.AddTransient<IPropertyFavoriteRepository, PropertyFavoriteRepository>();

            #endregion
        }
    }
}
