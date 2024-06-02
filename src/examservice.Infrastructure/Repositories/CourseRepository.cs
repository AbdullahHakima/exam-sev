using examservice.Domain.Entities;
using examservice.Infrastructure.Data;
using examservice.Infrastructure.Interfaces;
using ExamService.Infrastructure.Bases;
using Microsoft.EntityFrameworkCore;

namespace examservice.Infrastructure.Repositories;

public class CourseRepository : GenericRepositoryAsync<Course>, ICourseRepository
{
    private readonly DbSet<Course> _courses;
    public CourseRepository(ApplicationDbContext context) : base(context)
    {
        _courses = context.Set<Course>();
    }
}
