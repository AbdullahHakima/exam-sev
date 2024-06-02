using examservice.Domain.Entities;

namespace examservice.Service.Interfaces;

public interface IQuizService
{
    public Task<Quiz?> CreateQuiz(Quiz quizData);
    public Task UpdateQuiz(Quiz updatedQuizData);
    //Task<string> DeleteQuiz(Guid quizId);
    public Task<Quiz> GetQuizById(Guid quizId);
    public Task<List<Quiz>> GetAllQuizzes(Guid instructorId, Guid courseId);
    public Task<List<Quiz>> GetIncomingCourseQuizzes(Guid courseId);

    Task HandleEndedQuizzessAsync();
}
