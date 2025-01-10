using ExamApp.Data.Abstract;
using ExamApp.Entities;
using Microsoft.Extensions.Configuration;

namespace ExamApp.Data.Concrete.ADO.NET
{
    public class AdoNetExamRepository : AdoNetGenericRepository<Exam>, IExamRepository
    {
        public AdoNetExamRepository(IConfiguration configuration) : base(configuration) { }
    }
}
