using examservice.API.Base;
using examservice.Core.Features.Questions.Commands.Models.Add;
using examservice.Core.Features.Questions.Commands.Models.Remove;
using examservice.Core.Features.Questions.Commands.Models.Update;
using examservice.Core.Features.Questions.Queries.Models;
using examservice.Domain.Helpers.Dtos.Question;
using examservice.Domain.MetaData;
using Microsoft.AspNetCore.Mvc;

namespace examservice.API.Controllers;

[ApiController]
public class QuestionController : ApplicationController
{
    [HttpPost(Router.QuestionRouting.UploadQuestionsBank)]
    public async Task<IActionResult> UploadQuestionsBank([FromRoute] Guid courseId, IFormFile questionBank)
    {
        AddQuestionBankDto dto = new AddQuestionBankDto
        {
            Questionsbank = questionBank
        };
        var respone = await Mediator.Send(new AddQuestionBankCommandModel { AddQuestionBankDto = dto, courseId = courseId });
        return NewResult(respone);
    }
    [HttpPost(Router.QuestionRouting.AddQuestion)]
    public async Task<IActionResult> AddQuestion([FromRoute] Guid courseId, [FromBody] AddQuestionDto questionDto)
    {
        var response = await Mediator.Send(new AddQuestionCommandModel { courseId = courseId, questionDto = questionDto });
        return NewResult(response);
    }
    [HttpPut(Router.QuestionRouting.UpdateQuestion)]
    public async Task<IActionResult> UpdateQuestion([FromRoute] Guid questionId, [FromBody] UpdateQuestionDto questionDto)
    {
        var response = await Mediator.Send(new UpdateQuestionCommandModel { questionId = questionId, questionDto = questionDto });
        return NewResult(response);
    }
    [HttpDelete(Router.QuestionRouting.DeleteQuestion)]
    public async Task<IActionResult> RemoveQuestion([FromRoute] Guid questionId)
    {
        var response = await Mediator.Send(new RemoveQuestionCommandModel { questionId = questionId });
        return NewResult(response);
    }
    [HttpDelete(Router.QuestionRouting.DeleteBatchQuestions)]
    public async Task<IActionResult> RemoveBatchQuestions([FromForm] List<Guid> questionsIds)
    {
        var response = await Mediator.Send(new RemoveBatchQuestionsCommandModel { questionIds = questionsIds });
        return NewResult(response);
    }

    [HttpGet(Router.QuestionRouting.GenerateQuestionsBankFile)]
    public async Task<IActionResult> GenerateQuestionsBankFileAsync([FromRoute] Guid courseId)
    {
        var response = await Mediator.Send(new GetQuestionsBankQueryModel { CourseId = courseId });
        return NewResult(response);
    }
}

