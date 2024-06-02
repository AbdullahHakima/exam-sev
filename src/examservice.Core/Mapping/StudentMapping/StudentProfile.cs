using AutoMapper;

namespace examservice.Core.Mapping.StudentMapping;

public partial class StudentProfile : Profile
{
    public StudentProfile()
    {
        AddStudentMapping();
    }
}
