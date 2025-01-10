using AutoMapper;
using ExamApp.Entities;

namespace ExamApp.Business.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LessonDTO, Lesson>()
               .BeforeMap((src, dest) => dest.Id = Guid.NewGuid());
            CreateMap<StudentDTO, Student>()
              .BeforeMap((src, dest) => dest.Id = Guid.NewGuid());
            CreateMap<ExamDTO, Exam>()
              .BeforeMap((src, dest) => dest.Id = Guid.NewGuid());
        }
    }
}
