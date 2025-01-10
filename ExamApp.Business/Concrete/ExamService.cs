using AutoMapper;
using ExamApp.Business.Abstract;
using ExamApp.Business.Common;
using ExamApp.Business.Models;
using ExamApp.Data.Abstract;
using ExamApp.Entities;

namespace ExamApp.Business.Concrete
{
    public class ExamService : IExamManager
    {
        private readonly IMapper _mapper;
        private readonly IExamRepository _examRepository;

        public ExamService(IMapper mapper, IExamRepository examRepository)
        {
            _mapper = mapper;
            _examRepository = examRepository;
        }

        public async Task CreateAsync(ExamDTO examDTO)
        {
            var exam = _mapper.Map<Exam>(examDTO);

            Validation.Validate(exam);

            await _examRepository.CreateAsync(exam);
        }

        public async Task DeleteAsync(Guid id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null)
                throw new ArgumentNullException(nameof(exam));

            await _examRepository.DeleteAsync(exam);
        }

        public async Task<List<Exam>> GetAllAsync()
        {
            return await _examRepository.GetAllAsync();
        }

        public async Task<Exam> GetByIdAsync(Guid id)
        {
            return await _examRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Exam entity)
        {
            Validation.Validate(entity);
            await _examRepository.UpdateAsync(entity);
        }
    }
}
