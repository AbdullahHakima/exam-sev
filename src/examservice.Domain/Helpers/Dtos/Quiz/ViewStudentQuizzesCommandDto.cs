namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class ViewStudentQuizzesCommandDto
    {
        public Guid studentId { get; set; }
        public Guid courseId { get; set; }
    }
}
