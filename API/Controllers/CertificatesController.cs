// Controllers/CertificatesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

[ApiController]
[Route("api/[controller]")]
public class CertificatesController : ControllerBase
{
    private readonly DataContext _context;

    public CertificatesController(DataContext context)
    {
        _context = context;
    }

    // Получить все сертификаты
    [HttpGet]
    public async Task<IActionResult> GetAllCertificates()
    {
        var certificates = await _context.Certificates
            .Include(c => c.User)
            .Include(c => c.Course)
            .ToListAsync();
        return Ok(certificates);
    }

    // Получить сертификат по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCertificateById(int id)
    {
        var certificate = await _context.Certificates
            .Include(c => c.User)
            .Include(c => c.Course)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (certificate == null)
        {
            return NotFound("Certificate not found");
        }

        return Ok(certificate);
    }

    // Создать сертификат
    [HttpPost]
    public async Task<IActionResult> CreateCertificate([FromBody] Certificate certificate)
    {
        _context.Certificates.Add(certificate);
        await _context.SaveChangesAsync();
        return Ok(certificate);
    }

    // Обновить сертификат
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCertificate(int id, [FromBody] Certificate updatedCertificate)
    {
        var certificate = await _context.Certificates.FindAsync(id);

        if (certificate == null)
        {
            return NotFound("Certificate not found");
        }

        certificate.UserId = updatedCertificate.UserId;
        certificate.CourseId = updatedCertificate.CourseId;
        certificate.IssueDate = updatedCertificate.IssueDate;

        _context.Certificates.Update(certificate);
        await _context.SaveChangesAsync();
        return Ok(certificate);
    }

    // Удалить сертификат
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCertificate(int id)
    {
        var certificate = await _context.Certificates.FindAsync(id);

        if (certificate == null)
        {
            return NotFound("Certificate not found");
        }

        _context.Certificates.Remove(certificate);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Certificate deleted successfully" });
    }
}