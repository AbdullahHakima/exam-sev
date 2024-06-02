using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Question;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Questions.Commands.Models.Update
{
    public class UpdateQuestionCommandModel : IRequest<Response<string>>
    {
        public UpdateQuestionDto questionDto { get; set; }
        [FromRoute]
        public Guid questionId { get; set; }
    }
}
