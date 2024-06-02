using examservice.Domain.Helpers.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examservice.Domain.Entities;

public class Quiz
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime StartedDate { get; set; }
    public DateTime ClosedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int Capacity { get; set; }
    public decimal Grade { get; set; }
    public double Duration { get; set; }

    public QuizStatus Status { get; set; }

    public ICollection<Module> Modules { get; set; }
    [ForeignKey(nameof(InstructorId))]
    public Guid InstructorId { get; set; }
    public Instructor Instructor { get; set; }
    public ICollection<StudentQuizzes> StudentQuizzes { get; set; }
    [ForeignKey(nameof(CourseId))]
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
}
