using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class nullableSubContractorID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Employees_EmployeeId",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyPlanValues_SubContractors_SubContractorId",
                table: "WeeklyPlanValues");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_EmployeeId",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "ProjectManager",
                table: "Granders");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Granders");

            migrationBuilder.AddColumn<string>(
                name: "position",
                table: "WorkForces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "SubContractorId",
                table: "WeeklyPlanValues",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubWorkId",
                table: "SubContractors",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "RequestNo",
                table: "Granders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ApprovedBy",
                table: "Granders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Granders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "apprvedBy",
                table: "DailyLabors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "projectId",
                table: "Consultants",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "contractorId",
                table: "Consultants",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "consultantName",
                table: "Consultants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "projId",
                table: "AssignedWorkForces",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyPlanValues_SubContractors_SubContractorId",
                table: "WeeklyPlanValues",
                column: "SubContractorId",
                principalTable: "SubContractors",
                principalColumn: "SubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyPlanValues_SubContractors_SubContractorId",
                table: "WeeklyPlanValues");

            migrationBuilder.DropColumn(
                name: "position",
                table: "WorkForces");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Granders");

            migrationBuilder.DropColumn(
                name: "apprvedBy",
                table: "DailyLabors");

            migrationBuilder.DropColumn(
                name: "consultantName",
                table: "Consultants");

            migrationBuilder.AlterColumn<int>(
                name: "SubContractorId",
                table: "WeeklyPlanValues",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubWorkId",
                table: "SubContractors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RequestNo",
                table: "Granders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedBy",
                table: "Granders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ProjectManager",
                table: "Granders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Granders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "projectId",
                table: "Consultants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "contractorId",
                table: "Consultants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "projId",
                table: "AssignedWorkForces",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_EmployeeId",
                table: "Incidents",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Employees_EmployeeId",
                table: "Incidents",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyPlanValues_SubContractors_SubContractorId",
                table: "WeeklyPlanValues",
                column: "SubContractorId",
                principalTable: "SubContractors",
                principalColumn: "SubId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
