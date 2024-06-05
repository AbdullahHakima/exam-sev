using examservice.API.Base;
using examservice.Core.Features.Quizzes.Commands.Models.Add;
using examservice.Core.Features.Quizzes.Commands.Models.Delete;
using examservice.Core.Features.Quizzes.Commands.Models.Updated;
using examservice.Core.Features.Quizzes.Queries.Models;
using examservice.Domain.Helpers.Dtos.Quiz;
using examservice.Domain.MetaData;
using Microsoft.AspNetCore.Mvc;

namespace ExamService.API.Controllers;

[ApiController]
public class QuizController : ApplicationController
{
    [HttpPost(Router.QuizRouting.CreateQuiz)]
    public async Task<IActionResult> CreateQuizAsync([FromRoute] Guid instructorId, [FromRoute] Guid courseId, [FromForm] CreateQuizDto quizDto)
    {
        var response = await Mediator.Send(new CreateQuizCommandModel
        {
            courseId = courseId,
            quizDto = quizDto,
            instructorId = instructorId
        });
        return NewResult(response);
    }

    [HttpPost(Router.QuizRouting.PublishQuiz)]
    public async Task<IActionResult> PublishQuizAsync([FromRoute] Guid courseId, [FromRoute] Guid quizId, [FromForm] PublishQuizDto quizDto)
    {
        var response = await Mediator.Send(new PublishQuizCommandModel { quizId = quizId, courseId = courseId, publishQuizDto = quizDto });
        return NewResult(response);
    }

    [HttpPost(Router.QuizRouting.EnrollToQuiz)]
    public async Task<IActionResult> EnrollToQuizAsync([FromBody] EnrollToQuizDto enrollDto)
    {
        var response = await Mediator.Send(new EnrollToQuizCommanModel { enrollDto = enrollDto });
        return NewResult(response);
    }
    [HttpPost(Router.QuizRouting.SubmitQuiz)]
    public async Task<IActionResult> SubmitQuizAsync([FromBody] SubmitQuizDto submitDto)
    {
        var response = await Mediator.Send(new SubmitQuizCommandModel { submitDto = submitDto });
        return NewResult(response);
    }
    [HttpPost(Router.QuizRouting.ViewStudentQuizDetails)]
    public async Task<IActionResult> ViewStudentQuizDetailsAsync([FromBody] GetStudentQuizDto dto)
    {
        var response = await Mediator.Send(new ViewStudentQuizQueryModel { StudentQuizDto = dto });
        return NewResult(response);
    }
    //Should be for both mobile and web 
    [HttpGet(Router.QuizRouting.ViewInstructorQuizzes)]
    public async Task<IActionResult> ViewInstructorQuizzesAsync([FromRoute] GetInstructorQuizzesDto dto)
    {
        var response = await Mediator.Send(new ViewInstructorQuizzesQueryModel { instructorQuizzesDto = dto });
        return NewResult(response);
    }

    [HttpPut(Router.QuizRouting.UpdateQuizDetails)]
    public async Task<IActionResult> UpdateQuizDeatilsAsync([FromRoute] UpdateQuizCommandDto updateDto, [FromForm] UpdateQuizDeatilsDto deatilsDto)
    {
        var response = await Mediator.Send(new UpdateQuizCommandModel { updateDto = updateDto, deatilsDto = deatilsDto });
        return NewResult(response);
    }
    [HttpDelete(Router.QuizRouting.DeleteQuiz)]
    public async Task<IActionResult> DeleteQuizAsync([FromRoute] DeleteQuizCommandDto deleteDto)
    {
        var response = await Mediator.Send(new DeleteQuizCommandModel { commandDto = deleteDto });
        return NewResult(response);
    }
    [HttpGet(Router.QuizRouting.InstructorQuizDetails)]
    public async Task<IActionResult> ViewInstructorQuizDeatilsAsync([FromRoute] ViewInstructorQuizDetailsCommandDto dto)
    {
        var response = await Mediator.Send(new ViewInstructorQuizDetailsQueryModel { CommandDto = dto });
        return NewResult(response);
    }
    [HttpPost(Router.QuizRouting.ViewStudentQuizzes)]
    public async Task<IActionResult> ViewStudentQuizzesAsync([FromBody] ViewStudentQuizzesCommandDto dto)
    {
        var response = await Mediator.Send(new ViewStudentQuizzesQueryModel { Command = dto });
        return NewResult(response);
    }

}
