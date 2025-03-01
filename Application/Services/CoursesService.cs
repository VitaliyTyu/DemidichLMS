using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services;

public interface ICoursesService
{
    Task<List<Course>> GetCourses();
}

public class CoursesService : ICoursesService
{
    private readonly DataContext _context;

    public CoursesService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetCourses()
    {
        return await _context.Courses.ToListAsync();
    }
}