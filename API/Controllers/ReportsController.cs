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
            .Select(r => new ReportDTO
            {
                Id = r.Id,
                UserId = r.UserId,
                CourseId = r.CourseId,
                Progress = r.Progress
            })
            .ToListAsync();

        return Ok(reports);
    }

    // Получить отчет по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReportById(int id)
    {
        var report = await _context.Reports
            .Where(r => r.Id == id)
            .Select(r => new ReportDTO
            {
                Id = r.Id,
                UserId = r.UserId,
                CourseId = r.CourseId,
                Progress = r.Progress
            })
            .FirstOrDefaultAsync();

        if (report == null)
        {
            return NotFound("Report not found");
        }

        return Ok(report);
    }

    // Создать отчет
    [HttpPost]
    public async Task<IActionResult> CreateReport([FromBody] CreateReportDTO createReportDTO)
    {
        var report = new Report
        {
            UserId = createReportDTO.UserId,
            CourseId = createReportDTO.CourseId,
            Progress = createReportDTO.Progress
        };

        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        var reportDTO = new ReportDTO
        {
            Id = report.Id,
            UserId = report.UserId,
            CourseId = report.CourseId,
            Progress = report.Progress
        };

        return Ok(reportDTO);
    }

    // Обновить отчет
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReport(int id, [FromBody] UpdateReportDTO updateReportDTO)
    {
        var report = await _context.Reports.FindAsync(id);

        if (report == null)
        {
            return NotFound("Report not found");
        }

        report.Progress = updateReportDTO.Progress;

        _context.Reports.Update(report);
        await _context.SaveChangesAsync();

        var reportDTO = new ReportDTO
        {
            Id = report.Id,
            UserId = report.UserId,
            CourseId = report.CourseId,
            Progress = report.Progress
        };

        return Ok(reportDTO);
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