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
        var courses = await _context.Courses.Include(c => c.Modules).ToListAsync();
        return Ok(courses);
    }

    // Получить курс по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Modules)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
        {
            return NotFound("Course not found");
        }

        return Ok(course);
    }

    // Создать курс
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return Ok(course);
    }

    // Обновить курс
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course updatedCourse)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound("Course not found");
        }

        course.Title = updatedCourse.Title;
        course.Description = updatedCourse.Description;

        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
        return Ok(course);
    }

    // Удалить курс
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound("Course not found");
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Course deleted successfully" });
    }
}