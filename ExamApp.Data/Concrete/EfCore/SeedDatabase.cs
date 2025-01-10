using ExamApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExamApp.Data.Concrete.EfCore
{
    public class SeedDatabase
    {
        public static void Execute(IServiceCollection services)
        {
            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<EfCoreDbContext>();

                // Ensure that the database is created
                try
                {
                    context.Database.Migrate();

                    // Add seed data if the tables are created and no data exists
                    if (!context.Students.Any())
                    {
                        context.Students.AddRange(
                            new Student { Id = Guid.NewGuid(), Number = 1001, FirstName = "Rashad", LastName = "Ahmedov", Class = 10 },
                            new Student { Id = Guid.NewGuid(), Number = 1002, FirstName = "Leyla", LastName = "Huseynova", Class = 12 },
                            new Student { Id = Guid.NewGuid(), Number = 1003, FirstName = "Tural", LastName = "Mammadov", Class = 11 }
                            // Add other students
                        );
                        context.SaveChanges();
                    }

                    if (!context.Lessons.Any())
                    {
                        context.Lessons.AddRange(
                            new Lesson { Id = Guid.NewGuid(), LessonCode = "RIY", LessonName = "Mathematics", Class = 10, TeacherFirstName = "Araz", TeacherLastName = "Aliyev" },
                            new Lesson { Id = Guid.NewGuid(), LessonCode = "BIO", LessonName = "Biology", Class = 11, TeacherFirstName = "Leyla", TeacherLastName = "Ibrahimova" }
                            // Add other lessons
                        );
                        context.SaveChanges();
                    }

                    if (!context.Exams.Any())
                    {
                        var students = context.Students.ToList();
                        var lessons = context.Lessons.ToList();

                        // Add seed data for Exams using existing students and lessons
                        context.Exams.AddRange(
                            new Exam { Id = Guid.NewGuid(), LessonCode = lessons.First().LessonCode, StudentNumber = students.First().Number, ExamDate = new DateTime(2025, 1, 1), Grade = 9 },
                            new Exam { Id = Guid.NewGuid(), LessonCode = lessons.Skip(1).First().LessonCode, StudentNumber = students.Skip(1).First().Number, ExamDate = new DateTime(2025, 1, 2), Grade = 8 },
                            new Exam { Id = Guid.NewGuid(), LessonCode = lessons.First().LessonCode, StudentNumber = students.Skip(2).First().Number, ExamDate = new DateTime(2025, 1, 3), Grade = 7 }
                            // Add other exams
                        );
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions if needed
                    Console.WriteLine($"Error seeding database: {ex.Message}");
                }
            }
        }
    }
}
