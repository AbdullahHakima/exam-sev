using examservice.Core.Bases;
using MediatR;

namespace examservice.Core.Features.Questions.Commands.Models.Remove
{
    public class RemoveBatchQuestionsCommandModel : IRequest<Response<string>>
    {
        public List<Guid> questionIds { get; set; }
    }
}
