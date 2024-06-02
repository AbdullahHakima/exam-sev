using System.ComponentModel.DataAnnotations.Schema;

namespace examservice.Domain.Entities;

public class StudentCourses
{
    [ForeignKey(nameof(StudentId))]
    public Guid StudentId { get; set; }
    public Student student { get; set; }

    [ForeignKey(nameof(CourseId))]
    public Guid CourseId { get; set; }
    public Course course { get; set; }
}
