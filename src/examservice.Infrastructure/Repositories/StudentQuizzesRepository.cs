using examservice.Domain.Entities;
using examservice.Infrastructure.Data;
using examservice.Infrastructure.Interfaces;
using ExamService.Infrastructure.Bases;
using Microsoft.EntityFrameworkCore;

namespace examservice.Infrastructure.Repositories;

public class StudentQuizzesRepository : GenericRepositoryAsync<StudentQuizzes>, IStudentQuizzesRepository
{
    private readonly DbSet<StudentQuizzes> _studentQuizzes;
    public StudentQuizzesRepository(ApplicationDbContext context) : base(context)
    {
        _studentQuizzes = context.Set<StudentQuizzes>();
    }
}
