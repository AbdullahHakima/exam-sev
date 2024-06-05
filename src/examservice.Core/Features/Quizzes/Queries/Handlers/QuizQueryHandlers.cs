using AutoMapper;
using examservice.Core.Bases;
using examservice.Core.Features.Quizzes.Queries.Models;
using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Quiz;
using examservice.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace examservice.Core.Features.Quizzes.Queries.Handlers
{
    public class QuizQueryHandlers : ResponseHandler, IRequestHandler<ViewStudentQuizQueryModel, Response<ViewStudentQuizDto>>
                                                    , IRequestHandler<ViewInstructorQuizzesQueryModel, Response<List<ViewInstructorQuizzesDto>>>
                                                    , IRequestHandler<ViewInstructorQuizDetailsQueryModel, Response<ViewInstructorQuizDetailsDto>>
                                                    , IRequestHandler<ViewStudentQuizzesQueryModel, Response<List<ViewStudentQuizzesDto>>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IQuizService _quizService;
        private readonly IStudentQuizzesService _studentQuizzesService;
        private readonly IDistributedCache _cache;

        #endregion
        #region Constructors

        public QuizQueryHandlers(IMapper mapper, IQuizService quizService, IStudentQuizzesService studentQuizzesService, IDistributedCache cache)
        {
            _mapper = mapper;
            _quizService = quizService;
            _studentQuizzesService = studentQuizzesService;
            _cache = cache;
        }

        #endregion
        #region Methods

        public async Task<Response<ViewStudentQuizDto>> Handle(ViewStudentQuizQueryModel request, CancellationToken cancellationToken)
        {
            var inquiredQuiz = await _studentQuizzesService.GetStudentQuizAsync(request.StudentQuizDto.quizId, request.StudentQuizDto.studentId);
            if (inquiredQuiz == null) return NotFound<ViewStudentQuizDto>("Quiz not founded");
            var mappedQuizDetails = _mapper.Map<ViewStudentQuizDto>(inquiredQuiz);
            if (mappedQuizDetails.IsEnrolled)
                mappedQuizDetails.submission.Status = inquiredQuiz.AttemptStatus.ToString();
            return Success(mappedQuizDetails);
        }

        public async Task<Response<List<ViewInstructorQuizzesDto>>> Handle(ViewInstructorQuizzesQueryModel request, CancellationToken cancellationToken)
        {
            var inquiredQuizzes = await _quizService.GetAllQuizzes(request.instructorQuizzesDto.instructorId, request.instructorQuizzesDto.courseId);
            if (inquiredQuizzes == null) return NotFound<List<ViewInstructorQuizzesDto>>("there is no quizzes yet");
            var mappedQuizzes = _mapper.Map<List<ViewInstructorQuizzesDto>>(inquiredQuizzes);
            return Success(mappedQuizzes);
        }

        public async Task<Response<ViewInstructorQuizDetailsDto>> Handle(ViewInstructorQuizDetailsQueryModel request, CancellationToken cancellationToken)
        {
            var inquiredQuiz = await _quizService.GetQuizByIdAsync(request.CommandDto.quizId);
            if (inquiredQuiz == null) return NotFound<ViewInstructorQuizDetailsDto>("Quiz not founded");
            foreach (var module in inquiredQuiz.Modules)
            {
                var questions = await GetQuestionsFromCache(module.Id);
                if (questions != null)
                {
                    module.ModuleQuestions = questions.Select(q => new ModuleQuestion { Question = q }).ToList();
                }
            }
            var mappedQuiz = _mapper.Map<ViewInstructorQuizDetailsDto>(inquiredQuiz);
            return Success(mappedQuiz);
        }

        public async Task<Response<List<ViewStudentQuizzesDto>>> Handle(ViewStudentQuizzesQueryModel request, CancellationToken cancellationToken)
        {
            var inquiredQuizzes = await _studentQuizzesService.GetStudentQuizzesAsync(request.Command.courseId, request.Command.studentId);
            if (inquiredQuizzes == null) return NotFound<List<ViewStudentQuizzesDto>>("There no quizzes yet");
            var mappedQuizzes = _mapper.Map<List<ViewStudentQuizzesDto>>(inquiredQuizzes);
            return Success(mappedQuizzes);
        }

        private async Task<List<Question>> GetQuestionsFromCache(Guid moduleId)
        {
            var cacheKey = $"ModuleQuestions:{moduleId}";
            var serializedQuestions = await _cache.GetStringAsync(cacheKey);
            if (string.IsNullOrEmpty(serializedQuestions))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<List<Question>>(serializedQuestions);
        }
        #endregion
    }
}
