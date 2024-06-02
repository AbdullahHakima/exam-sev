using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examservice.Domain.Entities;

public class Module
{
    [Key]
    public Guid Id { get; set; }
    public int? AssignedCapacity { get; set; }
    public string Name { get; set; }
    public decimal TotalGrade { get; set; }
    [ForeignKey(nameof(QuizId))]
    public Guid? QuizId { get; set; }
    public virtual Quiz Quiz { get; set; }
    public virtual ICollection<ModuleQuestion> ModuleQuestions { get; set; }
    public virtual ICollection<Submission> Submissions { get; set; }
    public ICollection<StudentQuizzes> studentModuleQuizzes { get; set; }

}
