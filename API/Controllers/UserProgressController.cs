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
            .Select(p => new UserProgressDTO
            {
                Id = p.Id,
                UserId = p.UserId,
                CourseId = p.CourseId,
                ModuleId = p.ModuleId,
                IsCompleted = p.IsCompleted
            })
            .ToListAsync();

        return Ok(progress);
    }

    // Получить прогресс по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProgressById(int id)
    {
        var progress = await _context.UserProgresses
            .Where(p => p.Id == id)
            .Select(p => new UserProgressDTO
            {
                Id = p.Id,
                UserId = p.UserId,
                CourseId = p.CourseId,
                ModuleId = p.ModuleId,
                IsCompleted = p.IsCompleted
            })
            .FirstOrDefaultAsync();

        if (progress == null) return NotFound("Progress not found");

        return Ok(progress);
    }

    // Создать прогресс
    [HttpPost]
    public async Task<IActionResult> CreateProgress([FromBody] CreateUserProgressDTO createUserProgressDTO)
    {
        var progress = new UserProgress
        {
            UserId = createUserProgressDTO.UserId,
            CourseId = createUserProgressDTO.CourseId,
            ModuleId = createUserProgressDTO.ModuleId,
            IsCompleted = false
        };

        _context.UserProgresses.Add(progress);
        await _context.SaveChangesAsync();

        var progressDTO = new UserProgressDTO
        {
            Id = progress.Id,
            UserId = progress.UserId,
            CourseId = progress.CourseId,
            ModuleId = progress.ModuleId,
            IsCompleted = progress.IsCompleted
        };

        return Ok(progressDTO);
    }

    // Обновить прогресс
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProgress(int id, [FromBody] UpdateUserProgressDTO updateUserProgressDTO)
    {
        var progress = await _context.UserProgresses.FindAsync(id);

        if (progress == null) return NotFound("Progress not found");

        progress.IsCompleted = updateUserProgressDTO.IsCompleted;

        _context.UserProgresses.Update(progress);
        await _context.SaveChangesAsync();

        var progressDTO = new UserProgressDTO
        {
            Id = progress.Id,
            UserId = progress.UserId,
            CourseId = progress.CourseId,
            ModuleId = progress.ModuleId,
            IsCompleted = progress.IsCompleted
        };

        return Ok(progressDTO);
    }

    // Удалить прогресс
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProgress(int id)
    {
        var progress = await _context.UserProgresses.FindAsync(id);

        if (progress == null) return NotFound("Progress not found");

        _context.UserProgresses.Remove(progress);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Progress deleted successfully" });
    }
}