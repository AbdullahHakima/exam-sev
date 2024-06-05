using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Quiz;

namespace examservice.Core.Mapping.QuizMapping
{
    public partial class QuizProfile
    {
        public void ViewStudentQuizzesMapping()
        {
            CreateMap<StudentQuizzes, ViewStudentQuizzesDto>()
                .ForMember(dest => dest.quizId, opt => opt.MapFrom(src => src.QuizId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.quiz.Name))
                .ForMember(dest => dest.instructorName, opt => opt.MapFrom(src => src.quiz.Instructor.DisplayName))
                .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.AttemptStatus.ToString()));
        }
    }
}
