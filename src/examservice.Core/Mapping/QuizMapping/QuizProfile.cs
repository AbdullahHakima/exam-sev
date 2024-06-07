using AutoMapper;

namespace examservice.Core.Mapping.QuizMapping
{
    public partial class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateQuizMapping();
            ViewStudentQuizDetailsMapping();
            ViewInstructorQuizzesMapping();
            UpdateQuizDetailsMapping();
            ViewInstructorQuizDetailsMapping();
            ViewStudentQuizzesMapping();
            ViewQuizResultsMapping();
        }
    }
}
