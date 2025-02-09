// Controllers/UserProgressController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

[ApiController]
[Route("api/[controller]")]
public class UserProgressController : ControllerBase
{
    private readonly DataContext _context;

    public UserProgressController(DataContext context)
    {
        _context = context;
    }

    // Получить весь прогресс
    [HttpGet]
    public async Task<IActionResult> GetAllProgress()
    {
        var progress = await _context.UserProgresses
            .Include(p => p.User)
            .Include(p => p.Course)
            .Include(p => p.Module)
            .ToListAsync();
        return Ok(progress);
    }

    // Получить прогресс по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProgressById(int id)
    {
        var progress = await _context.UserProgresses
            .Include(p => p.User)
            .Include(p => p.Course)
            .Include(p => p.Module)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (progress == null)
        {
            return NotFound("Progress not found");
        }

        return Ok(progress);
    }

    // Создать прогресс
    [HttpPost]
    public async Task<IActionResult> CreateProgress([FromBody] UserProgress progress)
    {
        _context.UserProgresses.Add(progress);
        await _context.SaveChangesAsync();
        return Ok(progress);
    }

    // Обновить прогресс
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProgress(int id, [FromBody] UserProgress updatedProgress)
    {
        var progress = await _context.UserProgresses.FindAsync(id);

        if (progress == null)
        {
            return NotFound("Progress not found");
        }

        progress.UserId = updatedProgress.UserId;
        progress.CourseId = updatedProgress.CourseId;
        progress.ModuleId = updatedProgress.ModuleId;
        progress.IsCompleted = updatedProgress.IsCompleted;

        _context.UserProgresses.Update(progress);
        await _context.SaveChangesAsync();
        return Ok(progress);
    }

    // Удалить прогресс
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProgress(int id)
    {
        var progress = await _context.UserProgresses.FindAsync(id);

        if (progress == null)
        {
            return NotFound("Progress not found");
        }

        _context.UserProgresses.Remove(progress);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Progress deleted successfully" });
    }
}