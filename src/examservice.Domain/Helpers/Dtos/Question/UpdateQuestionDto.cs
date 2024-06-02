using examservice.Domain.Helpers.Dtos.Question.Option;
using examservice.Domain.Helpers.Enums;

namespace examservice.Domain.Helpers.Dtos.Question
{
    public class UpdateQuestionDto
    {
        public string Text { get; set; }
        public string ImageLink { get; set; } // ImageLink should not be nullable
        public decimal Points { get; set; }
        public decimal Duration { get; set; }
        public QuestionType Type { get; set; }
        public List<AddOptionDto> options { get; set; }
    }
}
