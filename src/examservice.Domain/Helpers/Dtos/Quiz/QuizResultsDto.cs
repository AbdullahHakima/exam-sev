using examservice.Domain.Helpers.Dtos.student;

namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class QuizResultsDto
    {
        public string QuizName { get; set; }
        public string CourseName { get; set; }
        public decimal QuizGrade { get; set; }
        public List<StudentQuizResultDto> studentQuizResult { get; set; }
    }
}
