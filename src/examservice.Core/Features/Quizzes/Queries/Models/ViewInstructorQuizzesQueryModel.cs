using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Quiz;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Quizzes.Queries.Models
{
    public class ViewInstructorQuizzesQueryModel : IRequest<Response<List<ViewInstructorQuizzesDto>>>
    {
        [FromRoute]
        public GetInstructorQuizzesDto instructorQuizzesDto { get; set; }
    }
}
