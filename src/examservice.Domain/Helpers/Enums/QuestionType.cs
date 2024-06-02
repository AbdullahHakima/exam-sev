using System.ComponentModel.DataAnnotations;

namespace examservice.Domain.Helpers.Enums;

public enum QuestionType
{
    [Display(Name = "Multiple Choice")]
    MultiChoice,
    [Display(Name = "True/False")]
    TrueFalse
}
