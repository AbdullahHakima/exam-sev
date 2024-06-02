using examservice.Infrastructure.Interfaces;
using examservice.Infrastructure.Repositories;
using ExamService.Infrastructure.Bases;
using Microsoft.Extensions.DependencyInjection;

namespace examservice.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IQuizRepository, QuizRepository>();
            services.AddTransient<IStudentQuizzesRepository, StudentQuizzesRepository>();
            services.AddTransient<ISubmissionsRepository, SubmissionRepository>();
            return services;
        }
    }
}
