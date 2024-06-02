using System.ComponentModel.DataAnnotations;

namespace examservice.Domain.Entities;
public class Student
{
    [Key]
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string AcademicId { get; set; }
    public ICollection<Submission> Submissions { get; set; }
    public ICollection<StudentQuizzes> StudentQuizzes { get; set; }
    public ICollection<StudentCourses> StudentCourses { get; set; }
}