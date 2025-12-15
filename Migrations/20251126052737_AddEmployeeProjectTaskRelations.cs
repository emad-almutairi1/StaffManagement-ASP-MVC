using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeProjectTaskRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "TaskItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "TaskItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_EmployeeId",
                table: "TaskItems",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_ProjectId",
                table: "TaskItems",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_EmployeeId",
                table: "Projects",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_EmployeeId",
                table: "Projects",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Employees_EmployeeId",
                table: "TaskItems",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Projects_ProjectId",
                table: "TaskItems",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_EmployeeId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Employees_EmployeeId",
                table: "TaskItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Projects_ProjectId",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_EmployeeId",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_ProjectId",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_Projects_EmployeeId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Projects");
        }
    }
}
