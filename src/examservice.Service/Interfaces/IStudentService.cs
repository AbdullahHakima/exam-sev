using examservice.Domain.Entities;

namespace examservice.Service.Interfaces;

public interface IStudentService
{
    public Task<List<Student>> GetStudentListAsync(Guid courseId);
    public Task<Student?> GetStudentByIdAsync(Guid studentId, Guid courseId);
    public Task AddStudentListAsync(List<Student> students);

    Task<Student> AddStudentAsync(Student student);
}
