using AutoMapper;

namespace examservice.Core.Mapping.QuestionOptionMapping
{
    public partial class OptionProfile : Profile
    {
        public OptionProfile()
        {
            AddQuestionOprionMapping();
            ViewOptionMapping();
            OptionsReportMapping();
        }
    }
}
