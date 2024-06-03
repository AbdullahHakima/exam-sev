namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class EnrollToQuizDto
    {
        public Guid quizId { get; set; }
        public Guid studentId { get; set; }
    }
}
