using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examservice.Domain.Entities;

public class Submission
{
    [Key]
    public Guid Id { get; set; }
    public decimal InitialGrade { get; set; }
    public decimal? FinalGrade { get; set; }
    public DateTime SubmitAt { get; set; }
    public TimeOnly TimeTaken { get; set; }
    [ForeignKey(nameof(StudentId))]
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public Module Module { get; set; }
    [ForeignKey(nameof(ModuleId))]
    public Guid ModuleId { get; set; }
}
