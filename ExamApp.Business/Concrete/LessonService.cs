using AutoMapper;
using ExamApp.Business.Abstract;
using ExamApp.Business.Common;
using ExamApp.Business.Models;
using ExamApp.Data.Abstract;
using ExamApp.Entities;

namespace ExamApp.Business.Concrete
{
    public class LessonService : ILessonManager
    {
        private readonly IMapper _mapper;
        private readonly ILessonRepository _lessonRepository;

        public LessonService(IMapper mapper, ILessonRepository lessonRepository)
        {
            _mapper = mapper;
            _lessonRepository = lessonRepository;
        }

        public async Task CreateAsync(LessonDTO lessonDTO)
        {
            var lesson = _mapper.Map<Lesson>(lessonDTO);

            Validation.Validate(lesson);

            await _lessonRepository.CreateAsync(lesson);
        }

        public async Task DeleteAsync(Guid id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
                throw new ArgumentNullException(nameof(lesson));

            await _lessonRepository.DeleteAsync(lesson);
        }

        public async Task<List<Lesson>> GetAllAsync()
        {
            return await _lessonRepository.GetAllAsync();
        }

        public async Task<Lesson> GetByIdAsync(Guid id)
        {
            return await _lessonRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Lesson lesson)
        {
            Validation.Validate(lesson);
            await _lessonRepository.UpdateAsync(lesson);
        }
    }
}
