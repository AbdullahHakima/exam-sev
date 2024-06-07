using examservice.Domain.Entities;
using examservice.Infrastructure.Interfaces;
using examservice.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace examservice.Service.Services;

public class QuizService : IQuizService
{

    #region Fields
    private readonly IQuizRepository _quizRepository;
    private readonly ISubmissionService _submissionService;
    private readonly IStudentQuizzesService _studentQuizzesService;


    #endregion

    #region Constructors
    public QuizService(IQuizRepository quizRepository, ISubmissionService submissionService, IStudentQuizzesService studentQuizzesService)
    {
        _quizRepository = quizRepository;
        _submissionService = submissionService;
        _studentQuizzesService = studentQuizzesService;
    }
    #endregion

    #region Methods
    public async Task<Quiz?> CreateQuizAsync(Quiz quizData)
    {

        return await _quizRepository.AddAsync(quizData);
    }

    public async Task DeleteQuizAsync(Quiz quiz)
    {
        await _quizRepository.DeleteAsync(quiz);
    }


    public async Task<List<Quiz>>? GetAllQuizzes(Guid instructorId, Guid courseId)
    {

        return _quizRepository.GetTableNoTracking()
                              .Include(q => q.Modules)
                                .ThenInclude(qm => qm.ModuleQuestions)
                                    .ThenInclude(mq => mq.Question)
                                        .ThenInclude(q => q.Options)
                              .Where(q => (q.InstructorId == instructorId && q.CourseId == courseId))
                              .ToList();
    }

    public async Task<List<Quiz>> GetIncomingCourseQuizzes(Guid courseId)
    {
        string[] includes = { "Instructor" };
        var inquiredQuizzes = await _quizRepository.FindAllAsync(q => (q.CourseId == courseId && q.StartedDate.ToLocalTime() >= DateTime.Now), includes);
        return inquiredQuizzes.ToList();
    }

    public async Task<Quiz?> GetQuizByIdAsync(Guid quizId)
    {
        return _quizRepository.GetTableNoTracking()
                              .Include(q => q.Instructor)
                              .Include(q => q.Course)
                              .Include(q => q.StudentQuizzes)
                              .Include(q => q.Modules)
                                .ThenInclude(qm => qm.ModuleQuestions)
                                    .ThenInclude(mq => mq.Question)
                                        .ThenInclude(q => q.Options)
                              .FirstOrDefault(q => q.Id == quizId);
    }




    public async Task HandleEndedQuizzessAsync()
    {
        var endedQuizzes = await _quizRepository.GetEndedQuizzesAsync();
        foreach (var quiz in endedQuizzes)
        {
            await _submissionService.UpdateStudentSubmissionAsync(quiz.Id);
            await _studentQuizzesService.GenerateQuizResultPdfFileAsync(quiz);
        }

    }

    public async Task UpdateQuizAsync(Quiz updatedQuizData)
    {
        await _quizRepository.UpdateAsync(updatedQuizData);
    }
    #endregion

}
