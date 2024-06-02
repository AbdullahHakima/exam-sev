using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Question.Option;

namespace examservice.Core.Mapping.QuestionOptionMapping
{
    public partial class OptionProfile
    {
        public void ViewOptionMapping()
        {
            CreateMap<Option, ViewOptionDto>();
        }
    }
}
