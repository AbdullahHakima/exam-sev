using examservice.Domain.Entities;
using examservice.Infrastructure.Interfaces;
using examservice.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace examservice.Service.Services;

public class QuestionService : IQuestionService
{
    #region Fields
    private readonly IQuestionRepository _questionRepository;
    private readonly IExcelProsessorService _excelProsessorService;
    #endregion
    #region Constructor
    public QuestionService(IQuestionRepository questionRepository, IExcelProsessorService excelProsessorService)
    {
        _questionRepository = questionRepository;
        _excelProsessorService = excelProsessorService;
    }
    #endregion
    #region Methods
    public async Task<Question?> GetQuestionByName(string name, Guid courseId)
    {
        string[] includes = { "Options", "ModuleQuestions" };
        var existQuestion = await _questionRepository.FindAsync(q => q.Text.Equals(name) && q.CourseId == courseId, includes);

        return existQuestion;
    }
    public async Task AddBulkQuestionsAsync(List<Question> questions)
    {
        await _questionRepository.AddRangeAsync(questions);
    }

    public async Task<Question?> AddQuestionAsync(Question question)
    {
        return await _questionRepository.AddAsync(question);
    }

    public async Task DeleteBulkQuestionsAsync(List<Question> updatedQuestions)
    {
        await _questionRepository.DeleteRangeAsync(updatedQuestions);
    }

    public async Task DeleteQuestionAsync(Question question)
    {
        await _questionRepository.DeleteAsync(question);
    }

    public async Task<List<Question>> GetAllQuestionsAsync(Guid courseId)
    {
        return await _questionRepository.GetTableNoTracking()
                                 .Include(q => q.Options)
                                 .Where(cq => cq.CourseId == courseId)
                                 .ToListAsync();
    }

    public async Task<Question> GetQuestionByIdAsync(Guid id)
    {
        var ExistedQuestion = _questionRepository.GetTableNoTracking()
                                                 .Include(q => q.Options)
                                                 .Where(q => q.Id == id).FirstOrDefault();
        if (ExistedQuestion == null)
            return null;
        return ExistedQuestion;
    }

    public async Task UpdateBulkQuestionsAsync(List<Question> updatedQuestions)
    {
        await _questionRepository.UpdateRangeAsync(updatedQuestions);
    }

    public async Task UpdateQuestionAsync(Question updatedQuestion)
    {
        await _questionRepository.UpdateAsync(updatedQuestion);
    }
    #endregion
}
