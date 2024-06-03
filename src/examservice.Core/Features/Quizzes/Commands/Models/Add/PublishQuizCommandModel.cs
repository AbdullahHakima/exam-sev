using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Quiz;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Quizzes.Commands.Models.Add
{
    public class PublishQuizCommandModel : IRequest<Response<string>>
    {
        [FromForm]
        public PublishQuizDto publishQuizDto { get; set; }
        public Guid quizId { get; set; }
        public Guid courseId { get; set; }
    }
}
