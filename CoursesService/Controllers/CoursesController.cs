using CoursesService.Models;
using CoursesService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoursesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _repository;

        public CoursesController(ICourseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _repository.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _repository.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            await _repository.AddCourseAsync(course);
            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Course updatedCourse)
        {
            var course = await _repository.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();

            updatedCourse.Id = id;
            await _repository.UpdateCourseAsync(updatedCourse);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _repository.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();

            await _repository.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}
