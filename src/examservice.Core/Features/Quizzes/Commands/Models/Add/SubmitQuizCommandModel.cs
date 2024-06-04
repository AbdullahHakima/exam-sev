using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Quiz;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Quizzes.Commands.Models.Add
{
    public class SubmitQuizCommandModel : IRequest<Response<string>>
    {
        [FromBody]
        public SubmitQuizDto submitDto { get; set; }
    }
}
