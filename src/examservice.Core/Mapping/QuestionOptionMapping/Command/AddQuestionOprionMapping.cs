using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Question.Option;

namespace examservice.Core.Mapping.QuestionOptionMapping
{
    public partial class OptionProfile
    {
        public void AddQuestionOprionMapping()
        {
            CreateMap<AddOptionDto, Option>();
        }
    }
}
