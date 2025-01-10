using ExamApp.Data.Abstract;
using ExamApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Data.Concrete.EfCore
{
    public class EfCoreExamRepository : EfCoreGenericRepository<Exam>, IExamRepository
    {
        private readonly EfCoreDbContext _context;
        public EfCoreExamRepository(EfCoreDbContext context) : base(context)
        {
            _context = context;
        } 
    }
}
