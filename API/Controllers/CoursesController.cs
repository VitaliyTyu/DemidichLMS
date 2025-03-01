// Controllers/CoursesController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly DataContext _context;

    public CoursesController(DataContext context)
    {
        _context = context;
    }

    // Получить все курсы
    [HttpGet]
    public async Task<IActionResult> GetAllCourses()
    {
        var courses = await _context.Courses
            .Select(c => new CourseDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description
            })
            .ToListAsync();

        return Ok(courses);
    }

    // Получить курс по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(int id)
    {
        var course = await _context.Courses
            .Where(c => c.Id == id)
            .Select(c => new CourseDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description
            })
            .FirstOrDefaultAsync();

        if (course == null) return NotFound("Course not found");

        return Ok(course);
    }

    // Создать курс
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDTO createCourseDTO)
    {
        var course = new Course
        {
            Title = createCourseDTO.Title,
            Description = createCourseDTO.Description
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        var courseDTO = new CourseDTO
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description
        };

        return Ok(courseDTO);
    }

    // Обновить курс
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDTO updateCourseDTO)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null) return NotFound("Course not found");

        course.Title = updateCourseDTO.Title;
        course.Description = updateCourseDTO.Description;

        _context.Courses.Update(course);
        await _context.SaveChangesAsync();

        var courseDTO = new CourseDTO
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description
        };

        return Ok(courseDTO);
    }

    // Удалить курс
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null) return NotFound("Course not found");

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Course deleted successfully" });
    }
}