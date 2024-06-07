using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Question;

namespace examservice.Core.Mapping.QuestionMapping
{
    public partial class QuestionProfile
    {
        public void GetQuestionsBankMapping()
        {
            CreateMap<Question, QuestionReportDto>()
                .ForMember(dest => dest.QuestionName, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.point, opt => opt.MapFrom(src => src.Points))
                .ForMember(dest => dest.option1, opt => opt.MapFrom(src => src.Options.ElementAtOrDefault(0).Text))
                .ForMember(dest => dest.option2, opt => opt.MapFrom(src => src.Options.ElementAtOrDefault(1).Text))
                .ForMember(dest => dest.option3, opt => opt.MapFrom(src => src.Options.ElementAtOrDefault(2).Text ?? string.Empty))
                .ForMember(dest => dest.option4, opt => opt.MapFrom(src => src.Options.ElementAtOrDefault(3).Text ?? string.Empty))
                .ForMember(dest => dest.option1Answer, opt => opt.MapFrom(src => src.Options.ElementAtOrDefault(0).IsCorrect))
                .ForMember(dest => dest.option2Answer, opt => opt.MapFrom(src => src.Options.ElementAtOrDefault(1).IsCorrect))
                .ForMember(dest => dest.option3Answer, opt => opt.MapFrom(src => src.Options.ElementAtOrDefault(2).IsCorrect))
                .ForMember(dest => dest.option4Answer, opt => opt.MapFrom(src => src.Options.ElementAtOrDefault(3).IsCorrect));

        }
    }
}
