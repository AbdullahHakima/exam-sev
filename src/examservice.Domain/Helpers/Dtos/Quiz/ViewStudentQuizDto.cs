using examservice.Domain.Helpers.Dtos.Submission;

namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class ViewStudentQuizDto
    {
        public Guid QuizId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime ClosedAt { get; set; }
        public decimal Grade { get; set; }
        public double Duration { get; set; }
        public string InstructorName { get; set; }
        public Guid ModuleId { get; set; }
        public bool IsEnrolled { get; set; }
        public ViewSubmissionDetailsDto submission { get; set; }
    }
}
