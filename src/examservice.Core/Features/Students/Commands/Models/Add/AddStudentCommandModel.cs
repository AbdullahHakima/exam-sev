using examservice.Core.Bases;
using examservice.Domain.Helpers.Dtos.student;
using MediatR;

namespace examservice.Core.Features.Students.Commands.Models.Add;

public class AddStudentCommandModel : IRequest<Response<string>>
{
    public AddStudentDto studentDto { get; set; }
}
