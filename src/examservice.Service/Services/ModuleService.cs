using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Module;
using examservice.Domain.Helpers.Enums;
using examservice.Infrastructure.Interfaces;
using examservice.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace examservice.Service.Services;

public class ModuleService : IModuleService
{
    #region Fields 
    private readonly IStudentQuizzesService _studentQuizzesService;
    private readonly IModuleRepository _moduleRepository;
    private readonly IDistributedCache _cache;
    private readonly SemaphoreSlim _asyncLock = new SemaphoreSlim(1, 1); // Initialize with initial request count 1 and maximum request count 1
    #endregion
    #region Constructors 
    public ModuleService(IStudentQuizzesService studentQuizzesService, IModuleRepository moduleRepository, IDistributedCache cache)
    {
        _moduleRepository = moduleRepository;
        _cache = cache;
        _studentQuizzesService = studentQuizzesService;
    }


    #endregion
    #region Methods

    public async Task<List<Module>> GenerateModulesAsync(List<Question> quizQuestions, int numberOfModules, int numberOfQuestionsPerModule, Guid courseId, Guid instructorId, bool? isSaved)
    {
        ValidateParameters(quizQuestions, numberOfModules, numberOfQuestionsPerModule);

        List<Question> shuffledQuestions = ShuffleQuestions(quizQuestions);
        List<Module> modules = CreateModules(shuffledQuestions, numberOfModules, numberOfQuestionsPerModule);

        var cacheEntry = new ModuleCacheEntry
        {
            Modules = modules,
            IsSaved = isSaved
        };

        await StoreModulesInCacheAsync(courseId, instructorId, cacheEntry);

        return modules;
    }

    private void ValidateParameters(List<Question> quizQuestions, int numberOfModules, int numberOfQuestionsPerModule)
    {
        if (quizQuestions == null || quizQuestions.Count == 0 || numberOfModules <= 0 || numberOfQuestionsPerModule <= 0)
            throw new ArgumentException("Invalid input parameters");

        int totalQuestions = numberOfModules * numberOfQuestionsPerModule;
        if (totalQuestions > quizQuestions.Count)
            throw new ArgumentException("Not enough questions available for the given parameters");
    }

    private List<Question> ShuffleQuestions(List<Question> quizQuestions)
    {
        Random rng = new Random();
        return quizQuestions.OrderBy(q => rng.Next()).ToList();
    }

    private List<Module> CreateModules(List<Question> shuffledQuestions, int numberOfModules, int numberOfQuestionsPerModule)
    {
        List<Module> modules = new List<Module>();

        for (int i = 0; i < numberOfModules; i++)
        {
            List<Question> moduleQuestions = shuffledQuestions.Skip(i * numberOfQuestionsPerModule).Take(numberOfQuestionsPerModule).ToList();

            Module module = new Module
            {
                Name = $"Module {i + 1}",
                ModuleQuestions = moduleQuestions.Select(q => new ModuleQuestion
                {
                    QuestionId = q.Id,
                    Question = new Question
                    {
                        Id = q.Id,
                        Text = q.Text,
                        Points = q.Points,
                        Duration = q.Duration,
                        ImageLink = q.ImageLink,
                        CourseId = q.CourseId,
                        Options = q.Options.Select(opt => new Option
                        {
                            Id = opt.Id,
                            Text = opt.Text,
                            IsCorrect = opt.IsCorrect
                        }).ToList()
                    }
                }).ToList(),
                TotalGrade = moduleQuestions.Sum(q => q.Points)
            };

            modules.Add(module);
        }

        return modules;
    }

    private async Task StoreModulesInCacheAsync(Guid courseId, Guid instructorId, ModuleCacheEntry cacheEntry)
    {
        try
        {
            string cacheKey = $"GeneratedModules:{courseId}:{instructorId}";
            string serializedCacheEntry = JsonConvert.SerializeObject(cacheEntry);

            await _asyncLock.WaitAsync();
            try
            {
                await _cache.SetStringAsync(cacheKey, serializedCacheEntry);
            }
            finally
            {
                _asyncLock.Release();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error storing modules in Redis cache: {ex.Message}");
            throw;
        }
    }


    public async Task<Module?> GetModuleById(Guid moduleId)
    {
        return _moduleRepository.GetTableNoTracking()
                                      .Include(m => m.ModuleQuestions)
                                        .ThenInclude(mq => mq.Question)
                                        .ThenInclude(q => q.Options)
                                      .Where(m => m.Id == moduleId)
                                      .FirstOrDefault();
    }

    public async Task<List<Module>> GetModulesByQuizId(Guid quizId)
    {
        return await _moduleRepository.GetTableNoTracking()
                                  .Include(m => m.ModuleQuestions)
                                      .ThenInclude(mq => mq.Question)
                                          .ThenInclude(q => q.Options)
                                  .Where(m => m.QuizId == quizId)
                                  .ToListAsync();
    }

    public async Task<List<Module>> SaveModules(List<Module> modules)
    {
        List<Module> addedModules = [];
        foreach (var module in modules)
        {
            _moduleRepository.DetachQuestions(module);
            addedModules.Add(await _moduleRepository.AddAsync(module));
        }
        return addedModules;
    }

    public async Task<Module> SaveModuleAsync(Module module)
    {
        _moduleRepository.DetachQuestions(module);
        return await _moduleRepository.AddAsync(module);
    }
    public async Task<int> AssignModulesToStudentAsync(List<Module> quizModules, List<Student> students, Guid quizId)
    {
        var moduleIds = quizModules.Select(m => m.Id).ToList();
        // Shuffle the module IDs to randomize the assignment
        var random = new Random();
        var shuffledModuleIds = moduleIds.OrderBy(x => random.Next()).ToList();

        var studentModuleAssignments = new List<StudentQuizzes>();

        for (int i = 0; i < students.Count; i++)
        {
            // Assign modules to each student
            var assignment = new StudentQuizzes
            {
                StudentId = students[i].Id,
                QuizId = quizId,
                ModuleId = shuffledModuleIds[i % shuffledModuleIds.Count],
                AttemptStatus = QuizAttemptStatus.NotAttempted,
                Enrolled = false
            };

            studentModuleAssignments.Add(assignment);
        }

        await _studentQuizzesService.AddStudentsToQuizAsync(studentModuleAssignments);
        foreach (var module in quizModules)
        {
            module.AssignedCapacity = studentModuleAssignments.Count(a => a.ModuleId == module.Id);
            await _moduleRepository.UpdateAsync(module);
        }
        return studentModuleAssignments.Count;
    }

    public async Task<Module?> GetStudentModuleByQuizId(Guid quizId, Guid studentId)
    {
        var studentQuiz = await _studentQuizzesService.GetStudentQuizAsync(quizId, studentId);

        return studentQuiz.Module;
    }


    #endregion


}
