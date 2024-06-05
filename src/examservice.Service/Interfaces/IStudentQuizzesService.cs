using examservice.Domain.Entities;
using examservice.Domain.Helpers.Enums;

namespace examservice.Service.Interfaces;

public interface IStudentQuizzesService
{
    Task UpdateAttemptStatusAsync(Guid quizId, Guid studentId, QuizAttemptStatus status);
    Task<StudentQuizzes> ViewQuizDetailsAsync(Guid quizId, Guid studentId);
    Task AddStudentsToQuizAsync(List<StudentQuizzes> studentQuizzes);
    Task<StudentQuizzes> GetStudentQuizAsync(Guid quizId, Guid studentId);
    Task UpdateStudentQuizAsync(StudentQuizzes studentQuizze);

    Task<List<StudentQuizzes>> GetStudentQuizzesAsync(Guid couresId, Guid studentId);
}
