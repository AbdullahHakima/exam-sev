namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class QuizResultFileDto
    {
        public byte[] fileContent { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
