using AutoMapper;
using examservice.Core.Bases;
using examservice.Core.Features.Quizzes.Queries.Models;
using examservice.Domain.Helpers.Dtos.Quiz;
using examservice.Service.Interfaces;
using MediatR;

namespace examservice.Core.Features.Quizzes.Queries.Handlers
{
    public class QuizQueryHandlers : ResponseHandler, IRequestHandler<ViewStudentQuizQueryModel, Response<ViewStudentQuizDto>>
                                                    , IRequestHandler<ViewInstructorQuizzesQueryModel, Response<List<ViewInstructorQuizzesDto>>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IQuizService _quizService;
        private readonly IStudentQuizzesService _studentQuizzesService;

        #endregion
        #region Constructors

        public QuizQueryHandlers(IMapper mapper, IQuizService quizService, IStudentQuizzesService studentQuizzesService)
        {
            _mapper = mapper;
            _quizService = quizService;
            _studentQuizzesService = studentQuizzesService;
        }

        #endregion
        #region Methods

        public async Task<Response<ViewStudentQuizDto>> Handle(ViewStudentQuizQueryModel request, CancellationToken cancellationToken)
        {
            var inquiredQuiz = await _studentQuizzesService.GetStudentQuizAsync(request.StudentQuizDto.quizId, request.StudentQuizDto.studentId);
            if (inquiredQuiz == null) return NotFound<ViewStudentQuizDto>("Quiz not founded");
            var mappedQuizDetails = _mapper.Map<ViewStudentQuizDto>(inquiredQuiz);
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
        #endregion
    }
}
