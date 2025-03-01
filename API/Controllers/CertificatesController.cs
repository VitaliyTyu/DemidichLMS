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
            .Select(c => new CertificateDTO
            {
                Id = c.Id,
                UserId = c.UserId,
                CourseId = c.CourseId,
                IssueDate = c.IssueDate
            })
            .ToListAsync();

        return Ok(certificates);
    }

    // Получить сертификат по ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCertificateById(int id)
    {
        var certificate = await _context.Certificates
            .Where(c => c.Id == id)
            .Select(c => new CertificateDTO
            {
                Id = c.Id,
                UserId = c.UserId,
                CourseId = c.CourseId,
                IssueDate = c.IssueDate
            })
            .FirstOrDefaultAsync();

        if (certificate == null) return NotFound("Certificate not found");

        return Ok(certificate);
    }

    // Создать сертификат
    [HttpPost]
    public async Task<IActionResult> CreateCertificate([FromBody] CreateCertificateDTO createCertificateDTO)
    {
        var certificate = new Certificate
        {
            UserId = createCertificateDTO.UserId,
            CourseId = createCertificateDTO.CourseId,
            IssueDate = DateTime.UtcNow
        };

        _context.Certificates.Add(certificate);
        await _context.SaveChangesAsync();

        var certificateDTO = new CertificateDTO
        {
            Id = certificate.Id,
            UserId = certificate.UserId,
            CourseId = certificate.CourseId,
            IssueDate = certificate.IssueDate
        };

        return Ok(certificateDTO);
    }

    // Обновить сертификат
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCertificate(int id, [FromBody] UpdateCertificateDTO updateCertificateDTO)
    {
        var certificate = await _context.Certificates.FindAsync(id);

        if (certificate == null) return NotFound("Certificate not found");

        certificate.IssueDate = updateCertificateDTO.IssueDate;

        _context.Certificates.Update(certificate);
        await _context.SaveChangesAsync();

        var certificateDTO = new CertificateDTO
        {
            Id = certificate.Id,
            UserId = certificate.UserId,
            CourseId = certificate.CourseId,
            IssueDate = certificate.IssueDate
        };

        return Ok(certificateDTO);
    }

    // Удалить сертификат
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCertificate(int id)
    {
        var certificate = await _context.Certificates.FindAsync(id);

        if (certificate == null) return NotFound("Certificate not found");

        _context.Certificates.Remove(certificate);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Certificate deleted successfully" });
    }
}