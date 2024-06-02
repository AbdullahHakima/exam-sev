using examservice.Domain.Entities;
using examservice.Infrastructure.Data;
using examservice.Infrastructure.Interfaces;
using ExamService.Infrastructure.Bases;
using Microsoft.EntityFrameworkCore;

namespace examservice.Infrastructure.Repositories;

public class QuestionRepository : GenericRepositoryAsync<Question>, IQuestionRepository
{
    private readonly DbSet<Question> _questions;
    public QuestionRepository(ApplicationDbContext context) : base(context)
    {
        _questions = context.Set<Question>();
    }


}
