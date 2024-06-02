using AutoMapper;

namespace examservice.Core.Mapping.QuizMapping
{
    public partial class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateQuizMapping();
        }
    }
}
