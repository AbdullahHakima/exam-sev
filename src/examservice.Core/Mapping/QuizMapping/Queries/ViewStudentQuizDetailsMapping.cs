using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Quiz;

namespace examservice.Core.Mapping.QuizMapping
{
    public partial class QuizProfile
    {
        public void ViewStudentQuizDetailsMapping()
        {
            CreateMap<StudentQuizzes, ViewStudentQuizDto>()
                .ForMember(dest => dest.StartedDate, opt => opt.MapFrom(src => src.quiz.StartedDate))
                .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.quiz.Id))
                .ForMember(dest => dest.ClosedAt, opt => opt.MapFrom(src => src.quiz.ClosedAt))
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.quiz.Instructor.DisplayName))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.quiz.Duration))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.quiz.Grade))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.quiz.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.quiz.Name))
                .ForMember(dest => dest.IsEnrolled, opt => opt.MapFrom(src => src.Enrolled))
                .ForMember(dest => dest.submission, opt => opt.MapFrom(src => src.submission));

        }
    }
}
