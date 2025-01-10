using ExamApp.Business.Abstract;
using ExamApp.Business.Models;
using ExamApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Api.Controllers
{
    [ApiController]
    [Route("api/lessons")]
    public class LessonsController : Controller
    {
        private readonly ILessonManager _lessonManager;

        public LessonsController(ILessonManager lessonManager)
        {
            _lessonManager = lessonManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lessons = await _lessonManager.GetAllAsync();
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var lesson = await _lessonManager.GetByIdAsync(id);

            if (lesson == null)
                return NotFound("Lesson not found.");

            return Ok(lesson);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LessonDTO lessonDTO)
        {
            await _lessonManager.CreateAsync(lessonDTO);
            return Ok(StatusCodes.Status200OK);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Lesson lesson)
        {
            var existingLesson = await _lessonManager.GetByIdAsync((Guid)lesson.Id);

            if (existingLesson == null)
                return NotFound("Lesson not found.");

            await _lessonManager.UpdateAsync(lesson);

            return Ok(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingLesson = await _lessonManager.GetByIdAsync(id);

            if (existingLesson == null)
                return NotFound("Lesson not found.");

            await _lessonManager.DeleteAsync(id);

            return Ok(StatusCodes.Status200OK);
        }
    }
}
