using Microsoft.AspNetCore.Builder;

namespace RealEstateAPI.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "RealEstateAPI");
            });
        }
    }
}
