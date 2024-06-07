using AutoMapper;

namespace examservice.Core.Mapping.QuestionMapping
{
    public partial class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            AddQuestionMapping();
            UpdateQuestionMapping();
            GetQuestionsBankMapping();
        }
    }
}
