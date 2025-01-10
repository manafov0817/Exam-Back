using ExamApp.Data.Abstract;
using ExamApp.Entities;
using Microsoft.Extensions.Configuration;

namespace ExamApp.Data.Concrete.ADO.NET
{
    public class AdoNetStudentRepository : AdoNetGenericRepository<Student>, IStudentRepository
    {
        public AdoNetStudentRepository(IConfiguration configuration) : base(configuration) { }
    }
}
