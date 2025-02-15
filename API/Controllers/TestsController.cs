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
        var tests = await _context.Tests
            .Select(t => new TestDTO
            {
                Id = t.Id,
                Questions = t.Questions,
                ModuleId = t.ModuleId
            })
            .ToListAsync();

        return Ok(tests);
    }

    // Получить тест по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestById(int id)
    {
        var test = await _context.Tests
            .Where(t => t.Id == id)
            .Select(t => new TestDTO
            {
                Id = t.Id,
                Questions = t.Questions,
                ModuleId = t.ModuleId
            })
            .FirstOrDefaultAsync();

        if (test == null)
        {
            return NotFound("Test not found");
        }

        return Ok(test);
    }

    // Создать тест
    [HttpPost]
    public async Task<IActionResult> CreateTest([FromBody] CreateTestDTO createTestDTO)
    {
        var test = new Test
        {
            Questions = createTestDTO.Questions,
            ModuleId = createTestDTO.ModuleId
        };

        _context.Tests.Add(test);
        await _context.SaveChangesAsync();

        var testDTO = new TestDTO
        {
            Id = test.Id,
            Questions = test.Questions,
            ModuleId = test.ModuleId
        };

        return Ok(testDTO);
    }

    // Обновить тест
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTest(int id, [FromBody] UpdateTestDTO updateTestDTO)
    {
        var test = await _context.Tests.FindAsync(id);

        if (test == null)
        {
            return NotFound("Test not found");
        }

        test.Questions = updateTestDTO.Questions;

        _context.Tests.Update(test);
        await _context.SaveChangesAsync();

        var testDTO = new TestDTO
        {
            Id = test.Id,
            Questions = test.Questions,
            ModuleId = test.ModuleId
        };

        return Ok(testDTO);
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