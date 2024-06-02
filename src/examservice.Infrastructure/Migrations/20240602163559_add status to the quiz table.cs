using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace examservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addstatustothequiztable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Quizs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Quizs");
        }
    }
}
