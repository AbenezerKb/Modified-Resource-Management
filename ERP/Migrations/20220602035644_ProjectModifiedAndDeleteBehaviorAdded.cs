using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class ProjectModifiedAndDeleteBehaviorAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceSheets_Employees_EmployeeId",
                table: "PerformanceSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceSheets_Projects_ProjectId",
                table: "PerformanceSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceSheets_SubContractors_SubContractorId",
                table: "PerformanceSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_CoordinatorId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employees_ManagerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Sites_SiteId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_Tasks_TaskId",
                table: "SubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyPlans_Projects_ProjectId",
                table: "WeeklyPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyPlanValues_Employees_EmployeeId",
                table: "WeeklyPlanValues");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyPlanValues_SubContractors_SubContractorId",
                table: "WeeklyPlanValues");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyResults_Employees_ApprovedById",
                table: "WeeklyResults");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyResults_WeeklyPlans_WeeklyPlanId",
                table: "WeeklyResults");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CoordinatorId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CoordinatorId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceSheets_Employees_EmployeeId",
                table: "PerformanceSheets",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceSheets_Projects_ProjectId",
                table: "PerformanceSheets",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceSheets_SubContractors_SubContractorId",
                table: "PerformanceSheets",
                column: "SubContractorId",
                principalTable: "SubContractors",
                principalColumn: "SubId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Sites_SiteId",
                table: "Projects",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_Tasks_TaskId",
                table: "SubTasks",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyPlans_Projects_ProjectId",
                table: "WeeklyPlans",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyPlanValues_Employees_EmployeeId",
                table: "WeeklyPlanValues",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyPlanValues_SubContractors_SubContractorId",
                table: "WeeklyPlanValues",
                column: "SubContractorId",
                principalTable: "SubContractors",
                principalColumn: "SubId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyResults_Employees_ApprovedById",
                table: "WeeklyResults",
                column: "ApprovedById",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyResults_WeeklyPlans_WeeklyPlanId",
                table: "WeeklyResults",
                column: "WeeklyPlanId",
                principalTable: "WeeklyPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceSheets_Employees_EmployeeId",
                table: "PerformanceSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceSheets_Projects_ProjectId",
                table: "PerformanceSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceSheets_SubContractors_SubContractorId",
                table: "PerformanceSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Sites_SiteId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_Tasks_TaskId",
                table: "SubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyPlans_Projects_ProjectId",
                table: "WeeklyPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyPlanValues_Employees_EmployeeId",
                table: "WeeklyPlanValues");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyPlanValues_SubContractors_SubContractorId",
                table: "WeeklyPlanValues");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyResults_Employees_ApprovedById",
                table: "WeeklyResults");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyResults_WeeklyPlans_WeeklyPlanId",
                table: "WeeklyResults");

            migrationBuilder.AddColumn<int>(
                name: "CoordinatorId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CoordinatorId",
                table: "Projects",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceSheets_Employees_EmployeeId",
                table: "PerformanceSheets",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceSheets_Projects_ProjectId",
                table: "PerformanceSheets",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceSheets_SubContractors_SubContractorId",
                table: "PerformanceSheets",
                column: "SubContractorId",
                principalTable: "SubContractors",
                principalColumn: "SubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_CoordinatorId",
                table: "Projects",
                column: "CoordinatorId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employees_ManagerId",
                table: "Projects",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Sites_SiteId",
                table: "Projects",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_Tasks_TaskId",
                table: "SubTasks",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyPlans_Projects_ProjectId",
                table: "WeeklyPlans",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyPlanValues_Employees_EmployeeId",
                table: "WeeklyPlanValues",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyPlanValues_SubContractors_SubContractorId",
                table: "WeeklyPlanValues",
                column: "SubContractorId",
                principalTable: "SubContractors",
                principalColumn: "SubId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyResults_Employees_ApprovedById",
                table: "WeeklyResults",
                column: "ApprovedById",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyResults_WeeklyPlans_WeeklyPlanId",
                table: "WeeklyResults",
                column: "WeeklyPlanId",
                principalTable: "WeeklyPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
