using examservice.Domain.Entities;
using ExamService.Infrastructure.Bases;

namespace examservice.Infrastructure.Interfaces;

public interface IQuizRepository : IGenericRepositoryAsync<Quiz>
{
    Task<List<Quiz>> GetEndedQuizzesAsync();
}
