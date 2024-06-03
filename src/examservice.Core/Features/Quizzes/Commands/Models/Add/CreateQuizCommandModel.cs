using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Quiz;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Quizzes.Commands.Models.Add
{
    public class CreateQuizCommandModel : IRequest<Response<string>>
    {
        [FromForm]
        public CreateQuizDto quizDto { get; set; }
        [FromRoute]
        public Guid instructorId { get; set; }
        [FromRoute]
        public Guid courseId { get; set; }
    }
}
