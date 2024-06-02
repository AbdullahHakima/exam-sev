using System.ComponentModel.DataAnnotations.Schema;

namespace examservice.Domain.Entities;

public class InstructorCourses
{
    [ForeignKey(nameof(InstructorId))]
    public Guid InstructorId { get; set; }
    public Instructor instructor { get; set; }

    [ForeignKey(nameof(CourseId))]
    public Guid CourseId { get; set; }
    public Course course { get; set; }
}
