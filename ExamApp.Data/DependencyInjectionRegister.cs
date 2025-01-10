using ExamApp.Data.Abstract;
using ExamApp.Data.Concrete.ADO.NET;
using ExamApp.Data.Concrete.ADO.NET.Helpers;
using ExamApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExamApp.Data
{
    public static class DependencyInjectionRegister
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEfCoreDatabase(configuration.GetConnectionString("DefaultConnection"));
            return services;
        }

        public static IServiceCollection AddEfCoreDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EfCoreDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ILessonRepository, EfCoreLessonRepository>();
            services.AddScoped<IStudentRepository, EfCoreStudentRepository>();
            services.AddScoped<IExamRepository, EfCoreExamRepository>();

            SeedDatabase.Execute(services);
            return services;
        }

        public static IServiceCollection AddAdoNetDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<ILessonRepository, AdoNetLessonRepository>();
            services.AddScoped<IStudentRepository, AdoNetStudentRepository>();
            services.AddScoped<IExamRepository, AdoNetExamRepository>();

            var dbInitializer = new DatabaseInitializer(connectionString);
            dbInitializer.InitializeDatabase();
            return services;
        }
    }
}
