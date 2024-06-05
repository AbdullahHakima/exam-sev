using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Quiz;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Quizzes.Queries.Models
{
    public class ViewStudentQuizzesQueryModel : IRequest<Response<List<ViewStudentQuizzesDto>>>
    {
        [FromBody]
        public ViewStudentQuizzesCommandDto Command { get; set; }
    }
}
