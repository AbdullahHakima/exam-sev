using AutoMapper;
using examservice.Core.Bases;
using examservice.Core.Features.Students.Commands.Models.Add;
using examservice.Domain.Entities;
using examservice.Service.Interfaces;
using MediatR;

namespace examservice.Core.Features.Students.Commands.Handlers;

public class StudentCommandHandlers : ResponseHandler, IRequestHandler<AddStudentCommandModel, Response<string>>
{
    #region Fields
    private readonly IMapper _mapper;
    private readonly IStudentService _studentService;
    private readonly ICourseService _courseService;
    #endregion

    #region Constructors
    public StudentCommandHandlers(IMapper mapper, IStudentService studentService, ICourseService courseService)
    {
        _mapper = mapper;
        _studentService = studentService;
        _courseService = courseService;
    }

    #endregion

    #region

    public async Task<Response<string>> Handle(AddStudentCommandModel request, CancellationToken cancellationToken)
    {
        var mappedStudent = _mapper.Map<Student>(request.studentDto);
        if (mappedStudent == null)
            return BadRequest("Something occurs while adding student data");
        else
            await _studentService.AddStudentAsync(mappedStudent);

        return Created("Successfully added ", mappedStudent);

    }

    #endregion

}
