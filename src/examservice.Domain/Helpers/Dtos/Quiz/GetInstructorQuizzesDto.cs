namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class GetInstructorQuizzesDto
    {
        public Guid courseId { get; set; }
        public Guid instructorId { get; set; }
    }
}
