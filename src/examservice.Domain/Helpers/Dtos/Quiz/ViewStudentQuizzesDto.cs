namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class ViewStudentQuizzesDto
    {
        public Guid quizId { get; set; }
        public string Name { get; set; }
        public string instructorName { get; set; }
        public string status { get; set; }
    }
}
