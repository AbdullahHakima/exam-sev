using DevExpress.XtraReports.UI;
using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Question;
using examservice.Infrastructure.Interfaces;
using examservice.Service.Interfaces;
using examservice.Service.Reports;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace examservice.Service.Services;

public class QuestionService : IQuestionService
{
    #region Fields
    private readonly IQuestionRepository _questionRepository;
    private readonly IExcelProsessorService _excelProsessorService;
    private static SemaphoreSlim _fileLock = new SemaphoreSlim(1);
    private readonly IWebHostEnvironment _hostingEnvironment;

    #endregion
    #region Constructor
    public QuestionService(IQuestionRepository questionRepository, IExcelProsessorService excelProsessorService, IWebHostEnvironment hostingEnvironment)
    {
        _questionRepository = questionRepository;
        _excelProsessorService = excelProsessorService;
        _hostingEnvironment = hostingEnvironment;
    }
    #endregion
    #region Methods
    public async Task<Question?> GetQuestionByName(string name, Guid courseId)
    {
        string[] includes = { "Options", "ModuleQuestions" };
        var existQuestion = await _questionRepository.FindAsync(q => q.Text.Equals(name) && q.CourseId == courseId, includes);

        return existQuestion;
    }
    public async Task AddBulkQuestionsAsync(List<Question> questions)
    {
        await _questionRepository.AddRangeAsync(questions);
    }

    public async Task<Question?> AddQuestionAsync(Question question)
    {
        return await _questionRepository.AddAsync(question);
    }

    public async Task DeleteBulkQuestionsAsync(List<Question> updatedQuestions)
    {
        await _questionRepository.DeleteRangeAsync(updatedQuestions);
    }

    public async Task DeleteQuestionAsync(Question question)
    {
        await _questionRepository.DeleteAsync(question);
    }

    public async Task<List<Question>> GetAllQuestionsAsync(Guid courseId)
    {
        return await _questionRepository.GetTableNoTracking()
                                 .Include(q => q.Options)
                                 .Where(cq => cq.CourseId == courseId)
                                 .ToListAsync();
    }

    public async Task<Question> GetQuestionByIdAsync(Guid id)
    {
        var ExistedQuestion = _questionRepository.GetTableNoTracking()
                                                 .Include(q => q.Options)
                                                 .Where(q => q.Id == id).FirstOrDefault();
        if (ExistedQuestion == null)
            return null;
        return ExistedQuestion;
    }

    public async Task UpdateBulkQuestionsAsync(List<Question> updatedQuestions)
    {
        await _questionRepository.UpdateRangeAsync(updatedQuestions);
    }

    public async Task UpdateQuestionAsync(Question updatedQuestion)
    {
        await _questionRepository.UpdateAsync(updatedQuestion);
    }

    public async Task<string?> GenereateQuestionsBankPdfFile(QuestionsBankReportDto bankDto)
    {
        var filePath = Path.Combine("wwwroot/CoursesQuestionsBank", $"{bankDto.CourseName}_QuestionsBank.pdf");
        if (File.Exists(filePath))
            File.Delete(filePath);

        return await GeneratePdfAsync(bankDto);
    }
    private async Task<XtraReport> CreateMainReportAsync(QuestionsBankReportDto bankDto)
    {
        // Load the main report layout
        var mainReport = new QuestionsBankReport();
        // Assign data to the main report
        mainReport.DataSource = new List<QuestionsBankReportDto> { bankDto };
        return mainReport;
    }

    private async Task<string?> GeneratePdfAsync(QuestionsBankReportDto bankDto)
    {
        await _fileLock.WaitAsync();
        try
        {
            var report = await CreateMainReportAsync(bankDto);
            if (_hostingEnvironment.WebRootPath == null)
            {
                return null;
            }

            // Generate a unique file name
            string fileName = $"{bankDto.CourseName}_QuestionsBank.pdf";
            string directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "CoursesQuestionsBank");
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

            // Write the byte array to a file asynchronously
            await using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await fileStream.WriteAsync(reportBytes, 0, reportBytes.Length);
            }

            return filePath;
        }
        finally
        {
            _fileLock.Release();
        }
    }

    #endregion
}
