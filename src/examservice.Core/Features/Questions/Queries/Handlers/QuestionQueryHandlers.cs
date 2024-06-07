using AutoMapper;
using examservice.Core.Bases;
using examservice.Core.Features.Questions.Queries.Models;
using examservice.Domain.Helpers.Dtos.Question;
using examservice.Service.Interfaces;
using MediatR;

namespace examservice.Core.Features.Questions.Queries.Handlers
{
    public class QuestionQueryHandlers : ResponseHandler, IRequestHandler<GetQuestionsBankQueryModel, Response<string>>
    {
        #region Fields
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;

        #endregion
        #region Constructors

        public QuestionQueryHandlers(IQuestionService questionService, IMapper mapper, ICourseService courseService)
        {
            _questionService = questionService;
            _mapper = mapper;
            _courseService = courseService;
        }
        #endregion
        #region Methods
        public async Task<Response<string>> Handle(GetQuestionsBankQueryModel request, CancellationToken cancellationToken)
        {
            var inquiredQuestions = await _questionService.GetAllQuestionsAsync(request.CourseId);
            if (inquiredQuestions.Count == 0) return NotFound<string>("there is no questions yet");
            var mappedQuestions = _mapper.Map<List<QuestionReportDto>>(inquiredQuestions);
            for (int i = 0; i < mappedQuestions.Count; i++)
            {
                var mappedQuestion = mappedQuestions[i];
                var originalQuestion = inquiredQuestions[i];

                mappedQuestion.Answers = originalQuestion.Options
                                            .Where(o => o.IsCorrect)
                                            .Select(o => o.Text)
                                            .ToList();
            }
            var course = await _courseService.GetByIdAsync(request.CourseId);
            var questionBank = new QuestionsBankReportDto
            {
                CourseName = course.Name,
                questions = mappedQuestions
            };
            var filepath = await _questionService.GenereateQuestionsBankPdfFile(questionBank);
            if (filepath is null)
                return BadRequest("Something occurs while generate your file");
            return Success(filepath);
        }
        #endregion
    }
}
