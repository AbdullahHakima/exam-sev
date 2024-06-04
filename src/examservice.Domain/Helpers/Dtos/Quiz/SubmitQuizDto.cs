namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class SubmitQuizDto
    {
        public Guid studentId { get; set; }
        public Guid quizId { get; set; }
        public Guid moduleId { get; set; }
        public TimeOnly TakenTime { get; set; }
        public decimal initialGrade { get; set; }
    }
}
