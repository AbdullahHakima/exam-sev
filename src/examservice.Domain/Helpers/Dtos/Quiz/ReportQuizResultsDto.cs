namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class ReportQuizResultsDto
    {
        public string QuizName { get; set; }
        public string CourseName { get; set; }
        public string StudentName { get; set; }
        public decimal StudentGrade { get; set; }
        public decimal QuizGrade { get; set; }
    }
}
