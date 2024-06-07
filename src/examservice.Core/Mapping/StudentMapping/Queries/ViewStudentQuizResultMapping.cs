using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.student;

namespace examservice.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {
        public void ViewStudentQuizResultMapping()
        {
            CreateMap<StudentQuizzes, StudentQuizResultDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.DisplayName))
                .ForMember(dest => dest.SubmitStatus, opt => opt.MapFrom(src => src.AttemptStatus))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.submission.FinalGrade));
        }
    }
}
