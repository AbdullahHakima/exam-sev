using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Module;
using examservice.Domain.Helpers.Dtos.Question;

namespace examservice.Core.Mapping.ModuleMapping
{
    public partial class ModuleProfile
    {
        public void ViewModuleQuestionsMapping()
        {
            CreateMap<ModuleQuestion, ViewQuestionDto>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Question.Text))
                .ForMember(dest => dest.ImageLink, opt => opt.MapFrom(src => src.Question.ImageLink))
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Question.Points))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Question.Type))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Question.Duration))
                .ForMember(dest => dest.options, opt => opt.MapFrom(src => src.Question.Options));

            CreateMap<Module, ViewQuizModuleDto>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.ModuleQuestions));

        }
    }
}
