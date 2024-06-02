using examservice.Service.Interfaces;
using examservice.Service.Services;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;

namespace examservice.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IHostApplicationBuilder builder)
        {

            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IExcelProsessorService, ExcelProsessorService>();
            services.AddTransient<IInstructorService, InstructorService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IQuizService, QuizService>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<ISubmissionService, SubmissionService>();
            services.AddTransient<IScheduledTaskService, ScheduledTaskService>();
            services.AddTransient<IStudentQuizzesService, StudentQuizzesService>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(postgreSqlOptions =>
                {
                    // Setup the connection factory
                    postgreSqlOptions.UseNpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
                }, new PostgreSqlStorageOptions
                {
                    SchemaName = "ScheduledTasks"
                }));

            services.AddHangfireServer();
            // Inject dependencies directly into the method
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();

                // Schedule the job
                recurringJobManager.AddOrUpdate(
                    "UpdateStudentSubmission",
                    () => serviceProvider.GetRequiredService<IScheduledTaskService>().UpdateSubmissionForEndedQuizzes(),
                    Cron.Hourly());  // Cron expression for every minute
            }

            return services;
        }

    }
}
