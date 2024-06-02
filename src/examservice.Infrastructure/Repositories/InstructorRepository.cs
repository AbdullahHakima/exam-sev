using examservice.Domain.Entities;
using examservice.Infrastructure.Data;
using examservice.Infrastructure.Interfaces;
using ExamService.Infrastructure.Bases;
using Microsoft.EntityFrameworkCore;

namespace examservice.Infrastructure.Repositories;

public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
{
    private readonly DbSet<Instructor> _instructors;
    public InstructorRepository(ApplicationDbContext context) : base(context)
    {
        _instructors = context.Set<Instructor>();
    }

}
