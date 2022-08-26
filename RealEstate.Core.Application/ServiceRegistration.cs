using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace RealEstate.Core.Application
{
    //Extension Methods - application of this design pattern Decorator
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddMediatR(Assembly.GetExecutingAssembly());   


            #region Services

            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IImprovementService, ImprovementService>();
            service.AddTransient<ISaleTypeService, SaleTypeService>();
            service.AddTransient<IPropertyTypeService, PropertyTypeService>();
            service.AddTransient<IPropertyService, PropertyService>();
            service.AddTransient<IPropertyFavoriteService, PropertyFavoriteService>();

            #endregion
        }
    }
}