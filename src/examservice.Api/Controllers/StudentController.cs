using examservice.API.Base;
using examservice.Core.Features.Students.Commands.Models.Add;
using examservice.Domain.Helpers.Dtos.student;
using examservice.Domain.MetaData;
using Microsoft.AspNetCore.Mvc;

namespace ExamService.API.Controllers
{
    [ApiController]
    public class StudentController : ApplicationController
    {
        [HttpPost(Router.StudentRouting.AddStudent)]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentDto dto)
        {
            var response = await Mediator.Send(new AddStudentCommandModel { studentDto = dto });
            return NewResult(response);
        }

    }
}
