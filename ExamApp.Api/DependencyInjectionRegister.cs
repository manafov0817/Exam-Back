using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;

namespace ExamApp.Api
{
    public static class DependencyInjectionRegister
    {
        public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggger();

            return services;
        }


        private static void AddSwaggger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ExamAPI", Version = "v1" });                
            });
        }
    }
}
