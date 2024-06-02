using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Question;

namespace examservice.Core.Mapping.QuestionMapping;

public partial class QuestionProfile
{
    public void AddQuestionMapping()
    {
        CreateMap<AddQuestionDto, Question>()
            .ForMember(dest => dest.ImageLink, opt => opt.MapFrom(src => src.ImageLink ?? ""))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
            .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.options));
    }
}
