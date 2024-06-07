using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Question.Option;

namespace examservice.Core.Mapping.QuestionOptionMapping
{
    public partial class OptionProfile
    {
        public void OptionsReportMapping()
        {
            CreateMap<Option, OptionReportDto>()
                .ForMember(dest => dest.OptionName, opt => opt.MapFrom(src => src.Text));

        }
    }
}
