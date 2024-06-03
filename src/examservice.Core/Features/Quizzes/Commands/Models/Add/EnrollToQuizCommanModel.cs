using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Module;
using examservice.Domain.Helpers.Dtos.Quiz;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Quizzes.Commands.Models.Add
{
    public class EnrollToQuizCommanModel : IRequest<Response<ViewQuizModuleDto>>
    {
        [FromBody]
        public EnrollToQuizDto enrollDto { get; set; }
    }
}
