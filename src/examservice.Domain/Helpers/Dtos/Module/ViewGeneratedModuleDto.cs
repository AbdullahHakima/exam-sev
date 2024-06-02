using examservice.Domain.Helpers.Dtos.Question;

namespace examservice.Domain.Helpers.Dtos.Module;

public class ViewGeneratedModuleDto
{
    public int? AssignedCapacity { get; set; }
    public string Name { get; set; }
    public decimal TotalGrade { get; set; }

    public List<ViewQuestionDto> Questions { get; set; }
}
