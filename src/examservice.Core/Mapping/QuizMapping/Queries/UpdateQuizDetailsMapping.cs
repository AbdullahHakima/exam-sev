using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Quiz;

namespace examservice.Core.Mapping.QuizMapping
{
    public partial class QuizProfile
    {
        public void UpdateQuizDetailsMapping()
        {
            CreateMap<UpdateQuizDeatilsDto, Quiz>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StartedDate, opt => opt.MapFrom(src => src.StartedDate))
                .ForMember(dest => dest.ClosedAt, opt => opt.MapFrom(src => src.ClosedAt))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        }
    }
}
