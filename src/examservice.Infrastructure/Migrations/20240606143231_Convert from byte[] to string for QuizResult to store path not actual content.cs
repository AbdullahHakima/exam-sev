using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace examservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConvertfrombytetostringforQuizResulttostorepathnotactualcontent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizResults",
                table: "Quizs");

            migrationBuilder.AddColumn<string>(
                name: "QuizResultsPath",
                table: "Quizs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizResultsPath",
                table: "Quizs");

            migrationBuilder.AddColumn<byte[]>(
                name: "QuizResults",
                table: "Quizs",
                type: "bytea",
                nullable: true);
        }
    }
}
