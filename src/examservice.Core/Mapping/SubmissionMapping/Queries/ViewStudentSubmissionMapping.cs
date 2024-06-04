using examservice.Domain.Entities;
using examservice.Domain.Helpers.Dtos.Submission;

namespace examservice.Core.Mapping.SubmissionMapping
{
    public partial class SubmissionProfile
    {
        public void ViewStudentSubmissionMapping()
        {
            CreateMap<Submission, ViewSubmissionDetailsDto>()
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.FinalGrade, opt => opt.MapFrom(src => src.FinalGrade))
                .ForMember(dest => dest.TimeTaken, opt => opt.MapFrom(src => src.TimeTaken))
                .ForMember(dest => dest.SubmitAt, opt => opt.MapFrom(src => src.SubmitAt));

        }
    }
}
