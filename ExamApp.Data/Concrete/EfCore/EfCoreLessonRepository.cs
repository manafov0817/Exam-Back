using ExamApp.Data.Abstract;
using ExamApp.Entities;

namespace ExamApp.Data.Concrete.EfCore
{
    public class EfCoreLessonRepository : EfCoreGenericRepository<Lesson>, ILessonRepository
    {
        public EfCoreLessonRepository(EfCoreDbContext context) : base(context)
        { }
    }
}
