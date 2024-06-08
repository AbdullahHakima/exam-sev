using examservice.Service.Interfaces;

namespace examservice.Service.Services;

public class ScheduledTaskService : IScheduledTaskService
{
    #region Fields
    private readonly IQuizService _quizService;


    #endregion

    #region Constructors
    public ScheduledTaskService(IQuizService quizService)
    {
        _quizService = quizService;
    }
    #endregion

    #region Methods


    public async Task GenerateQuizResultFileForEndedQuizzes()
    {
        await _quizService.HandleEndedQuizzessResultsAsync();
    }
    public async Task UpdateSubmissionForEndedQuizzes()
    {
        await _quizService.HandleEndedQuizzessSubmissionAsync();
    }
    #endregion
}
