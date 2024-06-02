namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class CreateQuizDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime ClosedAt { get; set; }
        public int Capacity { get; set; }
        public decimal Grade { get; set; }
        public double Duration { get; set; }
    }
}
