using ExamApp.Data.Abstract;
using ExamApp.Entities;
using Microsoft.Extensions.Configuration;

namespace ExamApp.Data.Concrete.ADO.NET
{
    public class AdoNetLessonRepository : AdoNetGenericRepository<Lesson>, ILessonRepository
    {
        public AdoNetLessonRepository(IConfiguration configuration) : base(configuration) { }
    }
}
