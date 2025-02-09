public class Question
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public Guid TestId { get; set; }
    public Test Test { get; set; }
    public List<Answer> Answers { get; set; } = new();
}