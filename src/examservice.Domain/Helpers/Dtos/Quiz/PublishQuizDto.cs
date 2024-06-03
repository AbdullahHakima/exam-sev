using System.ComponentModel.DataAnnotations;

namespace examservice.Domain.Helpers.Dtos.Quiz
{
    public class PublishQuizDto
    {
        public List<Guid>? studentIds { get; set; }
        [Required]
        public bool IsManual { get; set; }
    }
}
