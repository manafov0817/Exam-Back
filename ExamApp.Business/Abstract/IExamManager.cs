using ExamApp.Business.Models;
using ExamApp.Entities;

namespace ExamApp.Business.Abstract
{
    public interface IExamManager : IGenericManager<Exam, ExamDTO> { }
}
