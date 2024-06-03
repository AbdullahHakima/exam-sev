using AutoMapper;
using examservice.Core.Bases;
using examservice.Core.Features.Quizzes.Commands.Models.Add;
using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Module;
using examservice.Domain.Helpers.Enums;
using examservice.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace examservice.Core.Features.Quizzes.Commands.Handlers
{
    public class QuizCommandHandlers : ResponseHandler, IRequestHandler<CreateQuizCommandModel, Response<string>>
                                                      , IRequestHandler<PublishQuizCommandModel, Response<string>>
                                                      , IRequestHandler<EnrollToQuizCommanModel, Response<ViewQuizModuleDto>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IQuizService _quizService;
        private readonly IDistributedCache _cache;
        private readonly IModuleService _moduleService;
        private readonly IStudentService _studentService;
        private readonly IStudentQuizzesService _studentQuizzesService;
        #endregion

        #region Constructors
        public QuizCommandHandlers(IMapper mapper, IQuizService quizService, IModuleService moduleService, IDistributedCache cache, IStudentService studentService, IStudentQuizzesService studentQuizzesService)
        {
            _mapper = mapper;
            _quizService = quizService;
            _moduleService = moduleService;
            _cache = cache;
            _studentService = studentService;
            _studentQuizzesService = studentQuizzesService;
        }
        #endregion

        #region Methods

        public async Task<Response<string>> Handle(CreateQuizCommandModel request, CancellationToken cancellationToken)
        {
            var mappedQuiz = _mapper.Map<Quiz>(request.quizDto);
            mappedQuiz.InstructorId = request.instructorId;
            mappedQuiz.CourseId = request.courseId;
            var questions = new List<Question>();
            var addedModules = new List<Module>();
            mappedQuiz = await _quizService.CreateQuizAsync(mappedQuiz);

            if (mappedQuiz != null)
            {
                var cacheKey = $"GeneratedModules:{request.courseId}:{request.instructorId}";
                var serializedModules = await _cache.GetStringAsync(cacheKey);
                var moduleCacheEntry = JsonConvert.DeserializeObject<ModuleCacheEntry>(serializedModules);

                foreach (var module in moduleCacheEntry.Modules)
                {
                    module.Quiz = mappedQuiz;
                    module.QuizId = mappedQuiz.Id;
                }
                if (moduleCacheEntry.IsSaved is not null || moduleCacheEntry.IsSaved == false)
                {
                    foreach (var module in moduleCacheEntry.Modules)
                    {
                        questions.AddRange(module.ModuleQuestions.Select(mq => mq.Question).ToList());
                        module.ModuleQuestions.Clear();
                        addedModules.Add(await _moduleService.SaveModuleAsync(module));
                        await CacheQuestionsAsync(questions, module.Id, mappedQuiz.ClosedAt);
                        questions.Clear();
                    }
                }

                foreach (var module in addedModules)
                {
                    mappedQuiz.Modules.Add(module);
                }

                await _quizService.UpdateQuizAsync(mappedQuiz);

                return Created("Quiz is Created Successfully");
            }
            return UnprocessableEntity<string>("Can't process your request now, please try again");

        }


        public async Task CacheQuestionsAsync(List<Question> questions, Guid moduleId, DateTimeOffset expiration)
        {
            var cacheKey = $"ModuleQuestions:{moduleId}";
            var serializedQuestions = JsonConvert.SerializeObject(questions);
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = expiration
            };
            await _cache.SetStringAsync(cacheKey, serializedQuestions);

        }



        public async Task<Response<string>> Handle(PublishQuizCommandModel request, CancellationToken cancellationToken)
        {
            var existingQuiz = await _quizService.GetQuizByIdAsync(request.quizId);
            if (existingQuiz is null)
                return NotFound<string>("Quiz you are assign students to is not found");
            var quizModule = existingQuiz.Modules.ToList();
            int TotalQuizAssignment;
            List<Student> studentCourseList = [];

            //if IsManual is false then continue from course's stuudent list
            if (!request.publishQuizDto.IsManual)
            {
                studentCourseList = await _studentService.GetStudentListAsync(request.courseId);
                TotalQuizAssignment = await _moduleService.AssignModulesToStudentAsync(quizModule, studentCourseList, request.quizId);
            }
            else
            {

                foreach (var studentId in request.publishQuizDto.studentIds)
                {
                    studentCourseList.Add(await _studentService.GetStudentByIdAsync(studentId, request.courseId));
                }
                // else then the cilent need to assign this quiz to specific range of student to fo all student's course 
                TotalQuizAssignment = await _moduleService.AssignModulesToStudentAsync(quizModule, studentCourseList, request.quizId);
            }
            existingQuiz.Capacity = TotalQuizAssignment;
            existingQuiz.Status = QuizStatus.Published;
            await _quizService.UpdateQuizAsync(existingQuiz);
            return Success($"Quiz {existingQuiz.Name} is successfully published");
        }

        public async Task<Response<ViewQuizModuleDto>> Handle(EnrollToQuizCommanModel request, CancellationToken cancellationToken)
        {
            var inquiredModule = await _moduleService.GetStudentModuleByQuizId(request.enrollDto.quizId, request.enrollDto.studentId);
            if (inquiredModule == null)
                return NotFound<ViewQuizModuleDto>("Not Found module associated to you");
            //Incase of the modules has local cached questions
            var questions = await GetQuestionsFromCache(inquiredModule.Id);
            if (questions != null)
            {
                inquiredModule.ModuleQuestions = questions.Select(q => new ModuleQuestion { Question = q }).ToList();
            }
            var moduleMapped = _mapper.Map<ViewQuizModuleDto>(inquiredModule);
            return Success(moduleMapped);
        }

        public async Task<List<Question>> GetQuestionsFromCache(Guid moduleId)
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
