using ExamApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Data.Concrete.EfCore
{
    public class EfCoreDbContext : DbContext
    {
        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options) { }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
