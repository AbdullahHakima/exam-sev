using AutoMapper;
using examservice.Core.Bases;
using examservice.Core.Features.Questions.Commands.Models.Add;
using examservice.Core.Features.Questions.Commands.Models.Remove;
using examservice.Core.Features.Questions.Commands.Models.Update;
using examservice.Domain.Entities;
using examservice.Service.Interfaces;
using MediatR;

namespace examservice.Core.Features.Questions.Commands.Handlers;

public class QuestionCommandHandlers : ResponseHandler, IRequestHandler<AddQuestionBankCommandModel, Response<string>>
                                                      , IRequestHandler<AddQuestionCommandModel, Response<string>>
                                                      , IRequestHandler<UpdateQuestionCommandModel, Response<string>>
                                                      , IRequestHandler<RemoveQuestionCommandModel, Response<string>>
                                                      , IRequestHandler<RemoveBatchQuestionsCommandModel, Response<string>>
{
    #region Fields
    private readonly IMapper _mapper;
    private readonly IQuestionService _questionService;
    private readonly IExcelProsessorService _excelProsessorService;
    #endregion

    #region Constructors
    public QuestionCommandHandlers(IMapper mapper, IQuestionService questionService, IExcelProsessorService excelProsessorService)
    {
        _mapper = mapper;
        _questionService = questionService;
        _excelProsessorService = excelProsessorService;
    }
    #endregion

    #region Methods
    public async Task<Response<string>> Handle(AddQuestionBankCommandModel request, CancellationToken cancellationToken)
    {
        if (request.AddQuestionBankDto.Questionsbank == null || request.AddQuestionBankDto.Questionsbank.Length == 0)
        {
            return UnprocessableEntity<string>("This file is empty,unable to process it."); ;
        }
        using (var stream = request.AddQuestionBankDto.Questionsbank.OpenReadStream())
        {
            var questions = _excelProsessorService.ProcessExcelData(stream, request.courseId);
            if (questions != null)
            {
                List<Question> addedQuestions = [];
                foreach (var question in questions)
                {
                    var existingQuestion = await _questionService.GetQuestionByName(question.Text, question.CourseId);
                    if (existingQuestion is not null)
                        continue;
                    addedQuestions.Add(question);
                }
                if (addedQuestions.Count > 0)
                {
                    await _questionService.AddBulkQuestionsAsync(addedQuestions);
                    return Success("Questions uploaded successfully");
                }
                return UnprocessableEntity<string>("This questions list is already exsited");
            }
            return BadRequest("Error occure while uploading file , try it again!!");
        }
    }

    public async Task<Response<string>> Handle(AddQuestionCommandModel request, CancellationToken cancellationToken)
    {
        var existingQuestion = await _questionService.GetQuestionByName(request.questionDto.Text, request.courseId);
        if (existingQuestion is not null)
            return UnprocessableEntity<string>("This question already exist!");
        var mappedQuestion = _mapper.Map<Question>(request.questionDto);
        mappedQuestion.CourseId = request.courseId;
        if (mappedQuestion != null)
        {
            await _questionService.AddQuestionAsync(mappedQuestion);
            return Created("Question Successfully added", mappedQuestion);
        }
        return BadRequest("Something occurs while processign your request");
    }

    public async Task<Response<string>> Handle(UpdateQuestionCommandModel request, CancellationToken cancellationToken)
    {
        var existedQuestion = await _questionService.GetQuestionByIdAsync(request.questionId);
        if (existedQuestion is null) return NotFound<string>("Question not found");
        var mappedQuestion = _mapper.Map(request.questionDto, existedQuestion);
        await _questionService.UpdateQuestionAsync(mappedQuestion);
        return Success<string>("Question updated successfully", mappedQuestion);
    }

    public async Task<Response<string>> Handle(RemoveQuestionCommandModel request, CancellationToken cancellationToken)
    {
        var existedQuestion = await _questionService.GetQuestionByIdAsync(request.questionId);
        if (existedQuestion is null) return NotFound<string>("Question not founded");
        await _questionService.DeleteQuestionAsync(existedQuestion);
        return Success("Question removed successfully", existedQuestion);
    }

    public async Task<Response<string>> Handle(RemoveBatchQuestionsCommandModel request, CancellationToken cancellationToken)
    {
        var exsitedQuestions = new List<Question>();
        foreach (var questionId in request.questionIds)
        {
            var existedQuestion = await _questionService.GetQuestionByIdAsync(questionId);
            if (existedQuestion is null) continue;
            exsitedQuestions.Add(existedQuestion);
        }
        if (exsitedQuestions.Count > 0)
        {
            await _questionService.DeleteBulkQuestionsAsync(exsitedQuestions);
            return Success("Selected questions have removed successfully");
        }
        return NotFound<string>("there is not question founded");
    }
    #endregion
}
