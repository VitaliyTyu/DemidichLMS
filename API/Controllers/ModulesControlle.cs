// Controllers/ModulesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

[ApiController]
[Route("api/[controller]")]
public class ModulesController : ControllerBase
{
    private readonly DataContext _context;

    public ModulesController(DataContext context)
    {
        _context = context;
    }

    // Получить все модули
    [HttpGet]
    public async Task<IActionResult> GetAllModules()
    {
        var modules = await _context.Modules.Include(m => m.Course).ToListAsync();
        return Ok(modules);
    }

    // Получить модуль по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetModuleById(int id)
    {
        var module = await _context.Modules
            .Include(m => m.Course)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (module == null)
        {
            return NotFound("Module not found");
        }

        return Ok(module);
    }

    // Создать модуль
    [HttpPost]
    public async Task<IActionResult> CreateModule([FromBody] Module module)
    {
        _context.Modules.Add(module);
        await _context.SaveChangesAsync();
        return Ok(module);
    }

    // Обновить модуль
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateModule(int id, [FromBody] Module updatedModule)
    {
        var module = await _context.Modules.FindAsync(id);

        if (module == null)
        {
            return NotFound("Module not found");
        }

        module.Title = updatedModule.Title;
        module.Content = updatedModule.Content;
        module.CourseId = updatedModule.CourseId;

        _context.Modules.Update(module);
        await _context.SaveChangesAsync();
        return Ok(module);
    }

    // Удалить модуль
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModule(int id)
    {
        var module = await _context.Modules.FindAsync(id);

        if (module == null)
        {
            return NotFound("Module not found");
        }

        _context.Modules.Remove(module);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Module deleted successfully" });
    }
}