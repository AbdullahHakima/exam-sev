using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Quiz;

namespace examservice.Core.Mapping.SubmissionMapping
{
    public partial class SubmissionProfile
    {
        public void AddStudentSubmitMapping()
        {
            CreateMap<SubmitQuizDto, Submission>()
                .ForMember(dest => dest.InitialGrade, opt => opt.MapFrom(src => src.initialGrade))
                .ForMember(dest => dest.SubmitAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.TimeTaken, opt => opt.MapFrom(src => src.TakenTime))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.studentId))
                .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.moduleId));

        }
    }
}
