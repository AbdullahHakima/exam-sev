using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Quiz;
using examservice.Domain.Helpers.Enums;

namespace examservice.Core.Mapping.QuizMapping
{
    public partial class QuizProfile
    {
        public void CreateQuizMapping()
        {
            CreateMap<CreateQuizDto, Quiz>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => QuizStatus.draft))
                .ForMember(dest => dest.ClosedAt, opt => opt.MapFrom(src => src.ClosedAt.ToUniversalTime()))
                .ForMember(dest => dest.StartedDate, opt => opt.MapFrom(src => src.StartedDate.ToUniversalTime()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

        }
    }
}
