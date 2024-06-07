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
        public bool option1Answer { get; set; }
        public bool option2Answer { get; set; }
        public bool option3Answer { get; set; }
        public bool option4Answer { get; set; }
    }
}
