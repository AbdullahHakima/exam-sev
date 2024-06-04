using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Quiz;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Quizzes.Commands.Models.Delete
{
    public class DeleteQuizCommandModel : IRequest<Response<string>>
    {
        [FromRoute]
        public DeleteQuizCommandDto commandDto { get; set; }
    }
}
