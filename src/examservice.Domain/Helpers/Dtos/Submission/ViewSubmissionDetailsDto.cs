namespace examservice.Domain.Helpers.Dtos.Submission
{
    public class ViewSubmissionDetailsDto
    {
        public decimal? FinalGrade { get; set; }
        public DateTime SubmitAt { get; set; }
        public TimeOnly TimeTaken { get; set; }
        public string Status { get; set; }

    }
}
