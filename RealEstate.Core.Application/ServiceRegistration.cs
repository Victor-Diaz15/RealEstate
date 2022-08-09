using RealEstate.Core.Application.Interfaces.Services;
using RealEstate.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RealEstate.Core.Application
{
    //Extension Methods - application of this design pattern Decorator
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services

            service.AddTransient<IUserService, UserService>();

            #endregion
        }
    }
}