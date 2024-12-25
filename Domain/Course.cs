using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Course
    {
        [Column(TypeName = "uuid")]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
    }
}