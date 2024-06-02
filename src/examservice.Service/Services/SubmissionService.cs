using examservice.Domain.Entities;
using examservice.Domain.Helpers.Enums;
using examservice.Infrastructure.Interfaces;
using examservice.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace examservice.Service.Services;

public class SubmissionService : ISubmissionService
{
    #region Fields
    private readonly ISubmissionsRepository _submissionsRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IStudentQuizzesRepository _studentQuizzesRepository;


    #endregion


    #region Constructors
    public SubmissionService(ISubmissionsRepository submissionsRepository, IQuizRepository quizRepository, IStudentQuizzesRepository studentQuizzesRepository)
    {
        _submissionsRepository = submissionsRepository;
        _quizRepository = quizRepository;
        _studentQuizzesRepository = studentQuizzesRepository;
    }


    #endregion


    #region Methods
    public async Task<Submission> AddSubmission(Submission submission)
    {
        return await _submissionsRepository.AddAsync(submission);
    }

    public Task<List<Submission>> GetAllSubmissions(Guid courseId, Guid studentId, Guid moduleId)
    {
        throw new NotImplementedException();
    }

    public async Task<Submission?> GetSubmissionById(Guid submissionId)
    {
        return _submissionsRepository.GetTableNoTracking()
                                                     .Include(s => s.Module)
                                                     .Where(s => s.Id == submissionId)
                                                     .FirstOrDefault();
    }

    public async Task UpdateStudentSubmissionAsync(Guid quizId)
    {
        decimal fianlGrade;

        var quiz = await _quizRepository.FindAsync(q => q.Id == quizId, new[] { "StudentQuizzes" });

        var studentQuizzes = _studentQuizzesRepository.GetTableNoTracking()
                                                        .Include(s => s.Module)
                                                        .Include(s => s.submission)
                                                        .Where(sq => (sq.QuizId == quizId && sq.AttemptStatus == QuizAttemptStatus.Panding))
                                                        .ToList();
        foreach (var studentQuiz in studentQuizzes)
        {
            var studentGrade = CalculatefinalGrade(studentQuiz.submission.InitialGrade, studentQuiz.Module.TotalGrade, quiz.Grade);
            studentQuiz.submission.FinalGrade = studentGrade;
            await _submissionsRepository.UpdateAsync(studentQuiz.submission);

            if (studentQuiz.submission.SubmitAt > quiz.ClosedAt)
            {
                studentQuiz.AttemptStatus = QuizAttemptStatus.Late;
                await _studentQuizzesRepository.UpdateAsync(studentQuiz);
            }
            else if (studentQuiz.submission.SubmitAt <= quiz.ClosedAt)
            {
                studentQuiz.AttemptStatus = QuizAttemptStatus.Submitted;
                await _studentQuizzesRepository.UpdateAsync(studentQuiz);
            }
            else if (studentQuiz.submission is null)// there is no quiz submit for this student 
            {
                studentQuiz.AttemptStatus = QuizAttemptStatus.Missed;
                await _studentQuizzesRepository.UpdateAsync(studentQuiz);
            }

        }
    }
    public decimal CalculatefinalGrade(decimal totalGrade, decimal moduleGrade, decimal quizGrade)
    {
        decimal proportionalFactor = totalGrade * quizGrade;
        return proportionalFactor / moduleGrade;
    }
    #endregion
}
