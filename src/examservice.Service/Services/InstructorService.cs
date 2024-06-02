using examservice.Domain.Entities;
using examservice.Infrastructure.Interfaces;
using examservice.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace examservice.Service.Services;

public class InstructorService : IInstructorService
{
    #region Fields
    private readonly IInstructorRepository _instructorRepository;
    private readonly ICourseRepository _courseRepository;
    #endregion
    #region Constructors
    public InstructorService(IInstructorRepository instructorRepository, ICourseRepository courseRepository)
    {
        _instructorRepository = instructorRepository;
        _courseRepository = courseRepository;
    }
    #endregion
    #region Methods
    public async Task<Instructor> AddInstructorAsync(Instructor instructor)
    {
        return await _instructorRepository.AddAsync(instructor);
    }

    public Task<Instructor?> GetInstructorByIdAsync(Guid id)
    {
        return _instructorRepository.GetTableNoTracking()
                                          .FirstOrDefaultAsync(i => i.Id == id);
    }
    public async Task<List<Course>> GetInstructorCourses(Guid instructorId)
    {
        var instructorCourses = await _courseRepository.GetTableNoTracking()
            .Where(c => c.InstructorCourses.Any(ic => ic.InstructorId == instructorId))
            .ToListAsync();

        return instructorCourses;
    }
    #endregion
}
