using examservice.Domain.Entities;

namespace examservice.Service.Interfaces;

public interface ICourseService
{
    public Task<Course?> GetById(Guid id);
    public Task<Course> AddNew(Course course);
    public Task<Course?> GetByName(string name);


}
