namespace examservice.Domain.Helpers.Dtos.Question
{
    public class QuestionsBankReportDto
    {
        public string CourseName { get; set; }
        public List<QuestionReportDto> questions { get; set; }
    }
}
