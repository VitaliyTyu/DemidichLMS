// DTO/CourseDTO.cs

public class CourseDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}

public class CreateCourseDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class UpdateCourseDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
}

// DTO/ModuleDTO.cs
public class ModuleDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int CourseId { get; set; }
}

public class CreateModuleDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int CourseId { get; set; }
}

public class UpdateModuleDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
}


// DTO/TestDTO.cs
public class TestDTO
{
    public int Id { get; set; }
    public string Questions { get; set; }
    public int ModuleId { get; set; }
}

public class CreateTestDTO
{
    public string Questions { get; set; }
    public int ModuleId { get; set; }
}

public class UpdateTestDTO
{
    public string Questions { get; set; }
}

// DTO/CertificateDTO.cs
public class CertificateDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int CourseId { get; set; }
    public DateTime IssueDate { get; set; }
}

public class CreateCertificateDTO
{
    public string UserId { get; set; }
    public int CourseId { get; set; }
}

public class UpdateCertificateDTO
{
    public DateTime IssueDate { get; set; }
}


// DTO/ReportDTO.cs
public class ReportDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int CourseId { get; set; }
    public double Progress { get; set; }
}

public class CreateReportDTO
{
    public string UserId { get; set; }
    public int CourseId { get; set; }
    public double Progress { get; set; }
}

public class UpdateReportDTO
{
    public double Progress { get; set; }
}

// DTO/UserProgressDTO.cs
public class UserProgressDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
    public bool IsCompleted { get; set; }
}

public class CreateUserProgressDTO
{
    public string UserId { get; set; }
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
}

public class UpdateUserProgressDTO
{
    public bool IsCompleted { get; set; }
}