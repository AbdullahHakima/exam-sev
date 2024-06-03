using examservice.Domain.Entities;

namespace examservice.Service.Interfaces;

public interface IQuizService
{
    public Task<Quiz?> CreateQuizAsync(Quiz quizData);
    public Task UpdateQuizAsync(Quiz updatedQuizData);
    //Task<string> DeleteQuiz(Guid quizId);
    public Task<Quiz> GetQuizByIdAsync(Guid quizId);
    public Task<List<Quiz>> GetAllQuizzes(Guid instructorId, Guid courseId);
    public Task<List<Quiz>> GetIncomingCourseQuizzes(Guid courseId);

    Task HandleEndedQuizzessAsync();
}
