using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examservice.Domain.Entities;

public class Option
{
    [Key]
    public Guid Id { get; set; }
    public string Text { get; set; }
    public bool IsCorrect { get; set; } // Indicates if this option is correct
    [ForeignKey(nameof(QuestionId))]
    public Guid QuestionId { get; set; }
    public Question Question { get; set; }
}
