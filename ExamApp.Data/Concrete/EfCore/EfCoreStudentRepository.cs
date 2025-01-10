using ExamApp.Data.Abstract;
using ExamApp.Entities;

namespace ExamApp.Data.Concrete.EfCore
{
    public class EfCoreStudentRepository : EfCoreGenericRepository<Student>, IStudentRepository
    {
        public EfCoreStudentRepository(EfCoreDbContext context) : base(context)
        { }
    }
}
