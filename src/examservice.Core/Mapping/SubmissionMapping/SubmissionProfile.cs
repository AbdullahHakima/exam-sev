using AutoMapper;

namespace examservice.Core.Mapping.SubmissionMapping
{
    public partial class SubmissionProfile : Profile
    {
        public SubmissionProfile()
        {
            AddStudentSubmitMapping();
            ViewStudentSubmissionMapping();

        }
    }
}
