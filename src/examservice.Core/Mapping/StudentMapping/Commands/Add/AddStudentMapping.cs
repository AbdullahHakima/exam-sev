using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.student;

namespace examservice.Core.Mapping.StudentMapping;

public partial class StudentProfile
{
    public void AddStudentMapping()
    {
        CreateMap<AddStudentDto, Student>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
            .ForMember(dest => dest.AcademicId, opt => opt.MapFrom(src => src.AcademicId))
            .ForMember(dest => dest.StudentCourses, opt => opt.Ignore())
            .ForMember(dest => dest.Submissions, opt => opt.Ignore())
            .ForMember(dest => dest.StudentQuizzes, opt => opt.Ignore());
    }
}
