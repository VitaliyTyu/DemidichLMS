// Models/User.cs
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string FullName { get; set; }
    public string Role { get; set; } // Admin, Manager, Employee
}

// Models/Course.cs
public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Module> Modules { get; set; } = new();
}

// Models/Module.cs
public class Module
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; } // Может быть JSON или ссылка на файл
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public List<Test> Tests { get; set; } = new();
}

// Models/Test.cs
public class Test
{
    public int Id { get; set; }
    public string Questions { get; set; } // JSON с вопросами и ответами
    public int ModuleId { get; set; }
    public Module Module { get; set; }
}

// Models/Certificate.cs
public class Certificate
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime IssueDate { get; set; }
}

public class UserProgress
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public int ModuleId { get; set; }
    public Module Module { get; set; }
    public bool IsCompleted { get; set; }
}

// Models/Report.cs
public class Report
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public double Progress { get; set; } // Процент завершения курса
}