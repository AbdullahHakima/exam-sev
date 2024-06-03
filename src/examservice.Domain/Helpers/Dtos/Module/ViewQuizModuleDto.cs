using examservice.Domain.Helpers.Dtos.Question;

namespace examservice.Domain.Helpers.Dtos.Module
{
    public class ViewQuizModuleDto
    {
        public List<ViewQuestionDto> Questions { get; set; }
    }
}
