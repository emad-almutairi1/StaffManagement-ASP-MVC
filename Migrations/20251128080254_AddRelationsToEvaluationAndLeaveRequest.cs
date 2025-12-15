using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsToEvaluationAndLeaveRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EmployeeId",
                table: "Evaluations",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Employees_EmployeeId",
                table: "Evaluations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Employees_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Employees_EmployeeId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Employees_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_EmployeeId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Evaluations");
        }
    }
}
