using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Module;
using examservice.Domain.Helpers.Dtos.Question;
using examservice.Domain.Helpers.Dtos.Question.Option;

namespace examservice.Core.Mapping.ModuleMapping
{
    public partial class ModuleProfile
    {
        public void GenerateModuleMapping()
        {
            CreateMap<Module, ViewGeneratedModuleDto>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.AssignedCapacity, opt => opt.MapFrom(src => src.AssignedCapacity ?? 0))
               .ForMember(dest => dest.TotalGrade, opt => opt.MapFrom(src => src.TotalGrade))
               .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.ModuleQuestions.Select(moduleQuestion =>
            new ViewQuestionDto
            {
                Text = moduleQuestion.Question.Text,
                ImageLink = moduleQuestion.Question.ImageLink,
                Points = moduleQuestion.Question.Points,
                Duration = moduleQuestion.Question.Duration,
                Type = moduleQuestion.Question.Type.ToString(),
                options = moduleQuestion.Question.Options.Select(option =>
                    new ViewOptionDto
                    {
                        Text = option.Text,
                        IsCorrect = option.IsCorrect
                    }).ToList()
            })));




        }
    }
}
