using examservice.Domain.Helpers.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examservice.Domain.Entities;

public class Question
{
    [Key]
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string ImageLink { get; set; } // ImageLink should not be nullable
    public decimal Points { get; set; }
    public decimal Duration { get; set; }
    public QuestionType Type { get; set; }
    [ForeignKey(nameof(CourseId))]
    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; }
    public virtual ICollection<Option> Options { get; set; }
    public virtual ICollection<ModuleQuestion> ModuleQuestions { get; set; }
}

