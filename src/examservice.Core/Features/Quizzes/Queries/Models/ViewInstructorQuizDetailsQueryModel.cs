using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Quiz;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Quizzes.Queries.Models
{
    public class ViewInstructorQuizDetailsQueryModel : IRequest<Response<ViewInstructorQuizDetailsDto>>
    {
        [FromRoute]
        public ViewInstructorQuizDetailsCommandDto CommandDto { get; set; }
    }
}
