namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class UpdateQuizDeatilsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime ClosedAt { get; set; }
        public decimal Grade { get; set; }
        public double Duration { get; set; }
    }
}
