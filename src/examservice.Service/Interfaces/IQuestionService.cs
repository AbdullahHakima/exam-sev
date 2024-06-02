using examservice.Domain.Entities;

namespace examservice.Service.Interfaces;

public interface IQuestionService
{
    public Task AddBulkQuestionsAsync(List<Question> questions);
    public Task<Question?> AddQuestionAsync(Question question);
    public Task<Question> GetQuestionByIdAsync(Guid id);
    public Task<List<Question>> GetAllQuestionsAsync(Guid courseId);
    public Task UpdateQuestionAsync(Question updatedQuestion);
    public Task UpdateBulkQuestionsAsync(List<Question> updatedQuestions);
    public Task DeleteQuestionAsync(Question question);
    public Task DeleteBulkQuestionsAsync(List<Question> updatedQuestions);
    public Task<Question?> GetQuestionByName(string name, Guid courseId);
}
