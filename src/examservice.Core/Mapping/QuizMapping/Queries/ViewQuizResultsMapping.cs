using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Quiz;

namespace examservice.Core.Mapping.QuizMapping
{
    public partial class QuizProfile
    {
        public void ViewQuizResultsMapping()
        {
            CreateMap<Quiz, QuizResultsDto>()
                .ForMember(dest => dest.QuizName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name))
                .ForMember(dest => dest.studentQuizResult, opt => opt.MapFrom(src => src.StudentQuizzes));
        }
    }
}
