using examservice.Domain.Entities;
using examservice.Infrastructure.Data;
using examservice.Infrastructure.Interfaces;
using ExamService.Infrastructure.Bases;
using Microsoft.EntityFrameworkCore;

namespace examservice.Infrastructure.Repositories;

public class QuizRepository : GenericRepositoryAsync<Quiz>, IQuizRepository
{
    private readonly DbSet<Quiz> _quizs;
    public QuizRepository(ApplicationDbContext context) : base(context)
    {
        _quizs = context.Set<Quiz>();
    }

    public async Task<List<Quiz>> GetEndedQuizzesAsync()
    {
        return await _quizs.Where(q => q.ClosedAt.ToLocalTime() < DateTime.Now).ToListAsync();
    }
}
