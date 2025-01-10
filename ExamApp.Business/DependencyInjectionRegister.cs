using ExamApp.Business.Abstract;
using ExamApp.Business.Concrete;
using ExamApp.Business.Models;
using ExamApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExamApp.Business
{
    public static class DependencyInjectionRegister
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataLayer(configuration);

            services.AddScoped<ILessonManager, LessonService>();
            services.AddScoped<IStudentManager, StudentService>();
            services.AddScoped<IExamManager, ExamService>();

            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
