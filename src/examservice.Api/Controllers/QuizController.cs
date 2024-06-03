using examservice.API.Base;
using examservice.Core.Features.Quizzes.Commands.Models.Add;
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

}
