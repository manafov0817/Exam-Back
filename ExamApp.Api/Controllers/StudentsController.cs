using ExamApp.Business.Abstract;
using ExamApp.Business.Models;
using ExamApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : Controller
    {
        private readonly IStudentManager _studentManager;

        public StudentsController(IStudentManager studentManager)
        {
            _studentManager = studentManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentManager.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _studentManager.GetByIdAsync(id);

            if (student == null)
                return NotFound("Student not found.");

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentDTO studentDTO)
        {
            if (studentDTO == null)
                return BadRequest("Invalid student data.");

            await _studentManager.CreateAsync(studentDTO);
            return Ok(StatusCodes.Status200OK);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Student student)
        {
            if (student == null)
                return BadRequest("Invalid student data.");

            var existingStudent = await _studentManager.GetByIdAsync((Guid)student.Id);

            if (existingStudent == null)
                return NotFound("Student not found.");

            await _studentManager.UpdateAsync(student);
            return Ok(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingStudent = await _studentManager.GetByIdAsync(id);

            if (existingStudent == null)
                return NotFound("Student not found.");

            await _studentManager.DeleteAsync(id);
            return Ok(StatusCodes.Status200OK);
        }
    }
}
