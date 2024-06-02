using AutoMapper;
using examservice.Core.Bases;
using examservice.Core.Features.Modules.Commands.Models.Add;
using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Module;
using examservice.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace examservice.Core.Features.Modules.Commands.Handlers;

public class ModuleCommandHandlers : ResponseHandler, IRequestHandler<GenerateModulesCommandModel, Response<List<ViewGeneratedModuleDto>>>
{
    #region Fields
    private readonly IMapper _mapper;
    private readonly IModuleService _moduleService;
    private readonly IQuestionService _questionService;
    private readonly IExcelProsessorService _excelProsessorService;
    #endregion

    #region Constructors
    public ModuleCommandHandlers(IMapper mapper, IModuleService moduleService, IQuestionService questionService, IExcelProsessorService excelProsessorService)
    {
        _mapper = mapper;
        _moduleService = moduleService;
        _questionService = questionService;
        _excelProsessorService = excelProsessorService;
    }
    #endregion

    #region Methods
    public async Task<Response<List<ViewGeneratedModuleDto>>> Handle(GenerateModulesCommandModel request, CancellationToken cancellationToken)
    {
        List<Question> questionsBank = new();
        List<Module> generatedModules;
        var mappedModules = new List<ViewGeneratedModuleDto>();

        //process from the questions in db
        if (IsQuestionsSheetEmpty(request.moduleDto.questionBank))
        {
            questionsBank = await _questionService.GetAllQuestionsAsync(request.courseId);
            generatedModules = await GenerateModulesAsync(questionsBank, request);
            mappedModules = _mapper.Map<List<ViewGeneratedModuleDto>>(generatedModules);
            return Success(mappedModules, "Modules are generated Successfully");
        }

        questionsBank = await ProcessQuestionsSheetAsync(request, request.moduleDto.IsSaveUploadedQuestions);

        if (questionsBank == null)
        {
            return BadRequest<List<ViewGeneratedModuleDto>>(null, "Something occurred while uploading questions sheet");
        }

        generatedModules = await GenerateModulesAsync(questionsBank, request);
        mappedModules = _mapper.Map<List<ViewGeneratedModuleDto>>(generatedModules);
        return Success(mappedModules, "Modules are generated Successfully");
    }

    private bool IsQuestionsSheetEmpty(IFormFile questionsSheet)
    {
        return questionsSheet == null || questionsSheet.Length == 0;
    }

    private async Task<List<Question>> ProcessQuestionsSheetAsync(GenerateModulesCommandModel request, bool IsSaved)
    {

        var addedQuestions = new List<Question>();
        using var stream = request.moduleDto.questionBank.OpenReadStream();
        var questions = _excelProsessorService.ProcessExcelData(stream, request.courseId);

        if (questions == null)
        {
            return null;
        }

        //Save uploaded questions in db
        if (IsSaved)
        {
            foreach (var question in questions)
            {
                var existingQuestion = await _questionService.GetQuestionByName(question.Text, question.CourseId);
                if (existingQuestion is not null)
                    continue;
                addedQuestions.Add(question);
            }
            //save the question which is not already saved in db
            await SaveUploadedQuestions(addedQuestions);
        }
        // return the questions that processed from excel file  
        return questions;
    }

    private async Task<List<Module>> GenerateModulesAsync(List<Question> questionsBank, GenerateModulesCommandModel request)
    {
        return await _moduleService.GenerateModulesAsync(questionsBank, request.moduleDto.numberOfModules, request.moduleDto.numberOfQuestionPerModule, request.courseId, request.instructorId);
    }

    private async Task SaveUploadedQuestions(List<Question> uploadedQuestions)
    {
        await _questionService.AddBulkQuestionsAsync(uploadedQuestions);
    }



    #endregion
}
