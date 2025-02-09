// Controllers/ReportsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly DataContext _context;

    public ReportsController(DataContext context)
    {
        _context = context;
    }

    // Получить все отчеты
    [HttpGet]
    public async Task<IActionResult> GetAllReports()
    {
        var reports = await _context.Reports
            .Include(r => r.User)
            .Include(r => r.Course)
            .ToListAsync();
        return Ok(reports);
    }

    // Получить отчет по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReportById(int id)
    {
        var report = await _context.Reports
            .Include(r => r.User)
            .Include(r => r.Course)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (report == null)
        {
            return NotFound("Report not found");
        }

        return Ok(report);
    }

    // Создать отчет
    [HttpPost]
    public async Task<IActionResult> CreateReport([FromBody] Report report)
    {
        _context.Reports.Add(report);
        await _context.SaveChangesAsync();
        return Ok(report);
    }

    // Обновить отчет
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReport(int id, [FromBody] Report updatedReport)
    {
        var report = await _context.Reports.FindAsync(id);

        if (report == null)
        {
            return NotFound("Report not found");
        }

        report.UserId = updatedReport.UserId;
        report.CourseId = updatedReport.CourseId;
        report.Progress = updatedReport.Progress;

        _context.Reports.Update(report);
        await _context.SaveChangesAsync();
        return Ok(report);
    }

    // Удалить отчет
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReport(int id)
    {
        var report = await _context.Reports.FindAsync(id);

        if (report == null)
        {
            return NotFound("Report not found");
        }

        _context.Reports.Remove(report);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Report deleted successfully" });
    }
}