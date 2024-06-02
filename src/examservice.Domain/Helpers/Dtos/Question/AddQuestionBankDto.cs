using Microsoft.AspNetCore.Http;

namespace examservice.Domain.Helpers.Dtos.Question;

public class AddQuestionBankDto
{
    public IFormFile Questionsbank { get; set; }
}
