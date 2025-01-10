using ExamApp.Business.Abstract;
using ExamApp.Business.Models;
using ExamApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Api.Controllers
{
    [ApiController]
    [Route("api/exams")]
    public class ExamsController : Controller
    {
        private readonly IExamManager _examManager;

        public ExamsController(IExamManager examManager)
        {
            _examManager = examManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var exams = await _examManager.GetAllAsync();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var exam = await _examManager.GetByIdAsync(id);

            if (exam == null)
                return NotFound("Exam not found.");

            return Ok(exam);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExamDTO examDTO)
        {
            if (examDTO == null)
                return BadRequest("Invalid exam data.");

            await _examManager.CreateAsync(examDTO);
            return Ok(StatusCodes.Status200OK);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Exam exam)
        {
            if (exam == null)
                return BadRequest("Invalid exam data.");

            var existingExam = await _examManager.GetByIdAsync((Guid)exam.Id);

            if (existingExam == null)
                return NotFound("Exam not found.");

            await _examManager.UpdateAsync(exam);
            return Ok(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingExam = await _examManager.GetByIdAsync(id);

            if (existingExam == null)
                return NotFound("Exam not found.");

            await _examManager.DeleteAsync(id);
            return Ok(StatusCodes.Status200OK);
        }
    }
}
