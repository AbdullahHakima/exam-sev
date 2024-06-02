using AutoMapper;
using examservice.Core.Bases;
using examservice.Core.Features.Quizzes.Commands.Models.Add;
using examservice.Domain.Helpers.Dtos.Quiz;
using examservice.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace examservice.Core.Features.Quizzes.Commands.Handlers
{
    public class QuizCommandHandlers : ResponseHandler, IRequestHandler<CreateQuizCommandModel, Response<ViewQuizDto>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IQuizService _quizService;
        private readonly IDistributedCache _cache;
        #endregion

        #region Constructors
        public QuizCommandHandlers(IMapper mapper, IQuizService quizService)
        {
            _mapper = mapper;
            _quizService = quizService;
        }
        #endregion

        #region Methods

        public async Task<Response<ViewQuizDto>> Handle(CreateQuizCommandModel request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
