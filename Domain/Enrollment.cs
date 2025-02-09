public class Enrollment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.InProgress;
}

public enum EnrollmentStatus
{
    InProgress,
    Completed
}