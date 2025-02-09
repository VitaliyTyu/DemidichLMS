// Controllers/TestsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

[ApiController]
[Route("api/[controller]")]
public class TestsController : ControllerBase
{
    private readonly DataContext _context;

    public TestsController(DataContext context)
    {
        _context = context;
    }

    // Получить все тесты
    [HttpGet]
    public async Task<IActionResult> GetAllTests()
    {
        var tests = await _context.Tests.Include(t => t.Module).ToListAsync();
        return Ok(tests);
    }

    // Получить тест по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestById(int id)
    {
        var test = await _context.Tests
            .Include(t => t.Module)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (test == null)
        {
            return NotFound("Test not found");
        }

        return Ok(test);
    }

    // Создать тест
    [HttpPost]
    public async Task<IActionResult> CreateTest([FromBody] Test test)
    {
        _context.Tests.Add(test);
        await _context.SaveChangesAsync();
        return Ok(test);
    }

    // Обновить тест
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTest(int id, [FromBody] Test updatedTest)
    {
        var test = await _context.Tests.FindAsync(id);

        if (test == null)
        {
            return NotFound("Test not found");
        }

        test.Questions = updatedTest.Questions;
        test.ModuleId = updatedTest.ModuleId;

        _context.Tests.Update(test);
        await _context.SaveChangesAsync();
        return Ok(test);
    }

    // Удалить тест
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTest(int id)
    {
        var test = await _context.Tests.FindAsync(id);

        if (test == null)
        {
            return NotFound("Test not found");
        }

        _context.Tests.Remove(test);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Test deleted successfully" });
    }
}