using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Quiz;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Quizzes.Commands.Models.Updated
{
    public class UpdateQuizCommandModel : IRequest<Response<string>>
    {
        [FromRoute]
        public UpdateQuizCommandDto updateDto { get; set; }
        [FromForm]
        public UpdateQuizDeatilsDto deatilsDto { get; set; }
    }
}
