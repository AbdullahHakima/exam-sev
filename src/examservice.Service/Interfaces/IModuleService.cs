using examservice.Domain.Entities;

namespace examservice.Service.Interfaces;

public interface IModuleService
{
    /// <summary>
    /// that for generate modules that first shuffle all questions list
    /// then take the first N Questions and then take the next N Questions
    /// that depend on the numberOfQuestionsPerModule parameter. 
    /// </summary>
    /// <param name="quizQuestions"></param>
    /// <param name="numberOfModules"></param>
    /// <param name="numberOfQuestionsPerModule"></param>
    /// <returns>number of modules that have not stored in db yet</returns>
    public Task<List<Module>> GenerateModulesAsync(List<Question> quizQuestions, int numberOfModules, int numberOfQuestionsPerModule, Guid courseId, Guid instructorId, bool? IsSaved);

    ///// <summary>
    ///// used in case if the modules are already assigned to quiz and the quiz is already created,
    ///// assign the modules automatically by using shuffle of the modules questions and random assign to random users's course. 
    ///// </summary>
    ///// <param name="quizId"></param>
    ///// <param name="moduleIds"></param>
    ///// <returns>status of the process</returns>
    public Task<int> AssignModulesToStudentAsync(List<Module> quizModule, List<Student> students, Guid quizId);
    Task<List<Module?>> GetModulesByQuizId(Guid quizId);
    Task<Module?> GetModuleById(Guid moduleId);
    Task<List<Module>> SaveModules(List<Module> modules);
    Task<Module> SaveModuleAsync(Module module);
    Task<Module?> GetStudentModuleByQuizId(Guid quizId, Guid studentId);
}
