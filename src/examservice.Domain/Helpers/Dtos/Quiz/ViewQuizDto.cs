using examservice.Domain.Helpers.Dtos.Module;

namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class ViewQuizDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime ClosedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Capacity { get; set; }
        public decimal Grade { get; set; }
        public double Duration { get; set; }

        public string status { get; set; }// status of the quiz

        public List<ViewGeneratedModuleDto> Modules { get; set; }

        public string InstructorName { get; set; }

        public string CourseName { get; set; }
    }
}
