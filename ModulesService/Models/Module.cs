namespace ModulesService.Models;

public class Module
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; } // Может быть JSON или ссылка на файл
    public int CourseId { get; set; }
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