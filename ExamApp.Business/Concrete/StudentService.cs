using AutoMapper;
using ExamApp.Business.Abstract;
using ExamApp.Business.Common;
using ExamApp.Business.Models;
using ExamApp.Data.Abstract;
using ExamApp.Entities;

namespace ExamApp.Business.Concrete
{
    public class StudentService : IStudentManager
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;

        public StudentService(IMapper mapper, IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        public async Task CreateAsync(StudentDTO studentDTO)
        {
            var student = _mapper.Map<Student>(studentDTO);

            Validation.Validate(student);

            await _studentRepository.CreateAsync(student);
        }

        public async Task DeleteAsync(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            await _studentRepository.DeleteAsync(student);
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(Guid id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Student student)
        {
            Validation.Validate(student);

            await _studentRepository.UpdateAsync(student);
        }
    }
}
