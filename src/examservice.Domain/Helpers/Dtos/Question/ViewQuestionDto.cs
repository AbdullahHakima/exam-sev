using examservice.Domain.Helpers.Dtos.Question.Option;

namespace examservice.Domain.Helpers.Dtos.Question;

public class ViewQuestionDto
{
    public string Text { get; set; }
    public string ImageLink { get; set; } // ImageLink should not be nullable
    public decimal Points { get; set; }
    public decimal Duration { get; set; }
    public string Type { get; set; }
    public List<ViewOptionDto> options { get; set; }
}
