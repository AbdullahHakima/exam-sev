using examservice.Domain.Helpers.Dtos.Module;
using examservice.Domain.Helpers.Enums;

namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class ViewInstructorQuizDetailsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime ClosedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Capacity { get; set; }
        public decimal Grade { get; set; }
        public double Duration { get; set; }
        public QuizStatus Status { get; set; }
        public List<ViewGeneratedModuleDto> Modules { get; set; }
    }
}
