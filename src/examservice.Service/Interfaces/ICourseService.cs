using examservice.Domain.Entities;

namespace examservice.Service.Interfaces;

public interface ICourseService
{
    public Task<Course?> GetByIdAsync(Guid id);
    public Task<Course> AddNewAsync(Course course);
    public Task<Course?> GetByNameAsync(string name);


}
