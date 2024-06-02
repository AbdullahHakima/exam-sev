using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Question;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Questions.Commands.Models.Add;

public class AddQuestionCommandModel : IRequest<Response<string>>
{
    public AddQuestionDto questionDto { get; set; }
    [FromRoute]
    public Guid courseId { get; set; }
}
