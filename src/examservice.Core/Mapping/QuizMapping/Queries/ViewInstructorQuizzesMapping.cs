using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Quiz;

namespace examservice.Core.Mapping.QuizMapping
{
    public partial class QuizProfile
    {
        public void ViewInstructorQuizzesMapping()
        {
            CreateMap<Quiz, ViewInstructorQuizzesDto>()
                .ForMember(dest => dest.QuizStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
