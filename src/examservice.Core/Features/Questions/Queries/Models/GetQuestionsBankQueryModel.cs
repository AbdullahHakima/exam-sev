using examservice.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace examservice.Core.Features.Questions.Queries.Models
{
    public class GetQuestionsBankQueryModel : IRequest<Response<string>>
    {
        [FromRoute]
        public Guid CourseId { get; set; }
    }
}
