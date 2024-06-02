using examservice.Domain.Entities;
using examservice.Infrastructure.Data;
using examservice.Infrastructure.Interfaces;
using ExamService.Infrastructure.Bases;
using Microsoft.EntityFrameworkCore;

namespace examservice.Infrastructure.Repositories;

public class SubmissionRepository : GenericRepositoryAsync<Submission>, ISubmissionsRepository
{
    private readonly DbSet<Submission> _submissionRepository;
    public SubmissionRepository(ApplicationDbContext context) : base(context)
    {
        _submissionRepository = context.Set<Submission>();
    }
}
