using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffManagement.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEvaluatorNameFromEvaluation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvaluatorName",
                table: "Evaluations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EvaluatorName",
                table: "Evaluations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
