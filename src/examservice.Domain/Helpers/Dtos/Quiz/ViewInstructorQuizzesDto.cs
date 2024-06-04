namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class ViewInstructorQuizzesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string QuizStatus { get; set; }
    }
}
