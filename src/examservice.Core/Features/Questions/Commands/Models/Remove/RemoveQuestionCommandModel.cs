using examservice.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Questions.Commands.Models.Remove
{
    public class RemoveQuestionCommandModel : IRequest<Response<string>>
    {
        [FromRoute]
        public Guid questionId { get; set; }
    }
}
