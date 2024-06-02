using Microsoft.AspNetCore.Http;

namespace examservice.Domain.Helpers.Dtos.Module;

public class GenerateModuleDto
{
    public int numberOfModules { get; set; }
    public int numberOfQuestionPerModule { get; set; }
    public IFormFile? questionBank { get; set; }
    public bool IsSaveUploadedQuestions { get; set; }
}
