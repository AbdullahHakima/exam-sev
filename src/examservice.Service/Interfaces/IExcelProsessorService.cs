using examservice.Domain.Entities;

namespace examservice.Service.Interfaces;

public interface IExcelProsessorService
{
    public List<Question> ProcessExcelData(Stream ExcelData, Guid courseId);
}
