public class Test
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public List<Question> Questions { get; set; } = new();
}