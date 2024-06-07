namespace examservice.Domain.Helpers.Dtos.Question
{
    public class QuestionReportDto
    {
        public string QuestionName { get; set; }
        public string Type { get; set; }
        public decimal point { get; set; }
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        public List<string> Answers { get; set; }
    }
}
