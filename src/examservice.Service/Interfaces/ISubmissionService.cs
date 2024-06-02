using examservice.Domain.Entities;

namespace examservice.Service.Interfaces;

public interface ISubmissionService
{
    public Task<Submission> AddSubmission(Submission submission);
    public Task<Submission> GetSubmissionById(Guid submissionId);
    public Task<List<Submission>> GetAllSubmissions(Guid courseId, Guid studentId, Guid moduleId);//check later
    Task UpdateStudentSubmissionAsync(Guid quizId);
}
