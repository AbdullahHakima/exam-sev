namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class GetStudentQuizDto
    {
        public Guid studentId { get; set; }
        public Guid quizId { get; set; }
    }
}
