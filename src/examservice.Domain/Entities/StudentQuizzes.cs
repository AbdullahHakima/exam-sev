using examservice.Domain.Helpers.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace examservice.Domain.Entities;

public class StudentQuizzes
{
    [ForeignKey(nameof(StudentId))]
    public Guid StudentId { get; set; }
    public Student Student { get; set; }

    [ForeignKey(nameof(ModuleId))]
    public Guid ModuleId { get; set; }
    public Module Module { get; set; }

    [ForeignKey(nameof(QuizId))]
    public Guid QuizId { get; set; }
    public Quiz quiz { get; set; }

    [ForeignKey(nameof(SubmissionId))]
    public Guid? SubmissionId { get; set; }
    public Submission submission { get; set; }

    public bool Enrolled { get; set; }
    public QuizAttemptStatus AttemptStatus { get; set; }
}
