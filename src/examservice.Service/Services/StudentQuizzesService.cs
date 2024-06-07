using DevExpress.XtraReports.UI;
using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Quiz;
using examservice.Domain.Helpers.Dtos.student;
using examservice.Domain.Helpers.Enums;
using examservice.Infrastructure.Interfaces;
using examservice.Service.Interfaces;
using examservice.Service.Reports;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace examservice.Service.Services;

public class StudentQuizzesService : IStudentQuizzesService
{
    private readonly IStudentQuizzesRepository _studentQuizzesRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IQuizRepository _quizRepository;


    public StudentQuizzesService(IStudentQuizzesRepository studentQuizzesRepository, IWebHostEnvironment hostingEnvironment, IQuizRepository quizRepository)
    {
        _studentQuizzesRepository = studentQuizzesRepository;
        _hostingEnvironment = hostingEnvironment;
        _quizRepository = quizRepository;
    }

    public async Task AddStudentsToQuizAsync(List<StudentQuizzes> studentQuizzes)
    {
        await _studentQuizzesRepository.AddRangeAsync(studentQuizzes);
    }

    public async Task GenerateQuizResultPdfFileAsync(Quiz quiz)
    {
        var filePath = Path.Combine("wwwroot/QuizzesResults", $"{quiz.Name}.pdf");

        if (File.Exists(filePath))
            return;

        var studentQuizzes = await _studentQuizzesRepository.GetTableNoTracking()
                                                           .Include(sq => sq.quiz)
                                                                .ThenInclude(q => q.Course)
                                                            .Include(sq => sq.Student)
                                                            .Include(sq => sq.submission)
                                                            .Where(sq => sq.QuizId == quiz.Id)
                                                           .ToListAsync();
        if (studentQuizzes.Count == 0 || studentQuizzes.Any(sq => sq.AttemptStatus == QuizAttemptStatus.Panding))
            return;
        var quizResults = new List<QuizResultsDto>()
        {
            new QuizResultsDto{
            QuizGrade = quiz.Grade,
            QuizName = quiz.Name,
            CourseName = quiz.Course.Name,
            studentQuizResult = new List<StudentQuizResultDto>()
            }
        };
        var mappedStudentQuizResult = quizResults.First();
        foreach (var student in studentQuizzes)
        {
            if (student.Enrolled == true && student.submission is not null)
            {
                var result = new StudentQuizResultDto()
                {
                    StudentName = student.Student.DisplayName,
                    Grade = (decimal)student.submission.FinalGrade,
                    SubmitStatus = student.AttemptStatus.ToString()

                };
                mappedStudentQuizResult.studentQuizResult.Add(result);
            }
        }
        await GeneratePdfAsync(mappedStudentQuizResult, quiz);
    }
    private static SemaphoreSlim _fileLock = new SemaphoreSlim(1);

private async Task GeneratePdfAsync(QuizResultsDto resultsDto, Quiz quiz)
{
    await _fileLock.WaitAsync();
    try
    {
        var report = await CreateMainReportAsync(resultsDto);
        if (_hostingEnvironment.WebRootPath == null)
        {
            return;
        }
        // Generate a unique file name
        string fileName = $"{resultsDto.QuizName}.pdf";
        string directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "QuizzesResults");
        string filePath = Path.Combine(directoryPath, fileName);

        // Create the directory if it doesn't exist
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Export the report to a byte array
        byte[] reportBytes;
        using (MemoryStream stream = new MemoryStream())
        {
            report.ExportToPdf(stream);
            reportBytes = stream.ToArray();
        }
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            fileStream.Write(reportBytes, 0, reportBytes.Length);
        }
        Path.Combine("QuizzesResults", fileName);

        quiz.QuizResultsPath = filePath;
        await _quizRepository.UpdateAsync(quiz);
    }
    finally
    {
        _fileLock.Release();
    }
}

    private async Task<XtraReport> CreateMainReportAsync(QuizResultsDto resultsDto)
    {
        // Load the main report layout
        var mainReport = new QuizResultReport();
        // Assign data to the main report
        mainReport.DataSource = new List<QuizResultsDto> { resultsDto };

        var HeaderReport = new CommanReport();
        // Configure the subreport

        HeaderReport.Parameters["CourseName"].Value = resultsDto.CourseName;
        HeaderReport.Parameters["QuizName"].Value = resultsDto.QuizName;

        mainReport.xrSubreport1.ReportSource = HeaderReport;


        return mainReport;
    }

    public async Task<StudentQuizzes?> GetStudentQuizAsync(Guid quizId, Guid studentId)
    {
        return _studentQuizzesRepository.GetTableNoTracking()
                                                  .Include(sq => sq.quiz)
                                                    .ThenInclude(q => q.Instructor)
                                                  .Include(sq => sq.submission)
                                                  .Include(sq => sq.Module)
                                                   .SingleOrDefault(sq => (sq.QuizId == quizId && sq.StudentId == studentId));
    }

    public async Task<List<StudentQuizzes>> GetStudentQuizzesAsync(Guid couresId, Guid studentId)
    {
        var studentQuizzes = _studentQuizzesRepository.GetTableNoTracking()
                                                       .Include(sq => sq.quiz)
                                                            .ThenInclude(q => q.Instructor)
                                                       .Where(sq => (sq.StudentId == studentId && sq.quiz.CourseId == couresId))
                                                       .ToList();
        return studentQuizzes;
    }

    public async Task UpdateAttemptStatusAsync(Guid quizId, Guid studentId, QuizAttemptStatus status)
    {
        var studentQuiz = _studentQuizzesRepository.GetTableNoTracking().SingleOrDefault(sq => (sq.StudentId == studentId && sq.QuizId == quizId));
        studentQuiz.AttemptStatus = status;
        await _studentQuizzesRepository.UpdateAsync(studentQuiz);
    }

    public async Task UpdateStudentQuizAsync(StudentQuizzes studentQuizze)
    {
        await _studentQuizzesRepository.UpdateAsync(studentQuizze);
    }

    public async Task<StudentQuizzes?> ViewQuizDetailsAsync(Guid quizId, Guid studentId)
    {
        var inquiredQuiz = _studentQuizzesRepository.GetTableNoTracking()
                                                         .Include(sq => sq.submission)
                                                         .Include(sq => sq.Module)
                                                         .Include(sq => sq.quiz)
                                                            .ThenInclude(q => q.Instructor)//joined to get the quiz details and the student submission details
                                                         .SingleOrDefault(q => (q.QuizId == quizId && q.StudentId == studentId));
        return inquiredQuiz;
    }
}
