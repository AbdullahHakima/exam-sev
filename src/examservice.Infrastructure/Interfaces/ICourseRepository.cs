using examservice.Domain.Entities;
using ExamService.Infrastructure.Bases;

namespace examservice.Infrastructure.Interfaces;

public interface ICourseRepository : IGenericRepositoryAsync<Course>
{
}
