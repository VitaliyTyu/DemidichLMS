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
        var modules = await _context.Modules
            .Select(m => new ModuleDTO
            {
                Id = m.Id,
                Title = m.Title,
                Content = m.Content,
                CourseId = m.CourseId
            })
            .ToListAsync();

        return Ok(modules);
    }

    // Получить модуль по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetModuleById(int id)
    {
        var module = await _context.Modules
            .Where(m => m.Id == id)
            .Select(m => new ModuleDTO
            {
                Id = m.Id,
                Title = m.Title,
                Content = m.Content,
                CourseId = m.CourseId
            })
            .FirstOrDefaultAsync();

        if (module == null)
        {
            return NotFound("Module not found");
        }

        return Ok(module);
    }

    // Создать модуль
    [HttpPost]
    public async Task<IActionResult> CreateModule([FromBody] CreateModuleDTO createModuleDTO)
    {
        var module = new Module
        {
            Title = createModuleDTO.Title,
            Content = createModuleDTO.Content,
            CourseId = createModuleDTO.CourseId
        };

        _context.Modules.Add(module);
        await _context.SaveChangesAsync();

        var moduleDTO = new ModuleDTO
        {
            Id = module.Id,
            Title = module.Title,
            Content = module.Content,
            CourseId = module.CourseId
        };

        return Ok(moduleDTO);
    }

    // Обновить модуль
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateModule(int id, [FromBody] UpdateModuleDTO updateModuleDTO)
    {
        var module = await _context.Modules.FindAsync(id);

        if (module == null)
        {
            return NotFound("Module not found");
        }

        module.Title = updateModuleDTO.Title;
        module.Content = updateModuleDTO.Content;

        _context.Modules.Update(module);
        await _context.SaveChangesAsync();

        var moduleDTO = new ModuleDTO
        {
            Id = module.Id,
            Title = module.Title,
            Content = module.Content,
            CourseId = module.CourseId
        };

        return Ok(moduleDTO);
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