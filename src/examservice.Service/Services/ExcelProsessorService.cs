using examservice.Domain.Entities;
using examservice.Domain.Helpers.Enums;
using examservice.Service.Interfaces;
using OfficeOpenXml;

namespace examservice.Service.Services;

public class ExcelProsessorService : IExcelProsessorService
{
    public List<Question> ProcessExcelData(Stream excelStream, Guid courseId)
    {
        List<Question> importedQuestionList = new List<Question>();
        using (ExcelPackage package = new ExcelPackage(excelStream))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first worksheet

            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // Assuming first row contains headers
            {
                // Check if any of the cells in the row have values
                if (worksheet.Cells[row, 1].Value == null)
                    break;

                QuestionType questionType = (QuestionType)Enum.Parse(typeof(QuestionType), worksheet.Cells[row, 2].Value?.ToString());

                Question question = new Question
                {
                    Text = worksheet.Cells[row, 1].Value?.ToString(),
                    Type = questionType,
                    Options = new List<Option>(),
                    Points = decimal.Parse(worksheet.Cells[row, 8].Value?.ToString() ?? "1"), // Provide default value if cell value is null
                    Duration = decimal.Parse(worksheet.Cells[row, 9].Value?.ToString() ?? "1"),
                    ImageLink = worksheet.Cells[row, 10].Value?.ToString() ?? "no image",
                    CourseId = courseId // Set courseId passed from the front end, now hard-coded for testing
                };

                // Determine the number of options based on the question type
                int numOptions = (questionType == QuestionType.TrueFalse) ? 2 : 4;

                // Get correct answers, which may be multiple and comma-separated
                string correctAnswers = worksheet.Cells[row, 7].Value?.ToString();
                var correctAnswersSet = new HashSet<string>(correctAnswers?.Split(',').Select(a => a.Trim()), StringComparer.OrdinalIgnoreCase);

                // Fill options
                for (int optionIndex = 0; optionIndex < numOptions; optionIndex++)
                {
                    string optionText = worksheet.Cells[row, 3 + optionIndex].Value?.ToString();
                    if (!string.IsNullOrEmpty(optionText))
                    {
                        Option option = new Option { Text = optionText };

                        // Check if this option is in the set of correct answers
                        if (correctAnswersSet.Contains(optionText))
                        {
                            option.IsCorrect = true;
                        }

                        question.Options.Add(option);
                    }
                }

                importedQuestionList.Add(question);
            }
        }
        return importedQuestionList;
    }


}
