using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.Quiz;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Quizzes.Queries.Models
{
    public class ViewStudentQuizQueryModel : IRequest<Response<ViewStudentQuizDto>>
    {
        [FromBody]
        public GetStudentQuizDto StudentQuizDto { get; set; }
    }
}
