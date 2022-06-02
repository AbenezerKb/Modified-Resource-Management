using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class SubTaskRelationWithWeeklyPlanValueAddedDeletionRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyPlanValues_SubTasks_SubTaskId",
                table: "WeeklyPlanValues");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyResultValues_SubTasks_SubTaskId",
                table: "WeeklyResultValues");

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyPlanValues_SubTasks_SubTaskId",
                table: "WeeklyPlanValues",
                column: "SubTaskId",
                principalTable: "SubTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyResultValues_SubTasks_SubTaskId",
                table: "WeeklyResultValues",
                column: "SubTaskId",
                principalTable: "SubTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyPlanValues_SubTasks_SubTaskId",
                table: "WeeklyPlanValues");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyResultValues_SubTasks_SubTaskId",
                table: "WeeklyResultValues");

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyPlanValues_SubTasks_SubTaskId",
                table: "WeeklyPlanValues",
                column: "SubTaskId",
                principalTable: "SubTasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyResultValues_SubTasks_SubTaskId",
                table: "WeeklyResultValues",
                column: "SubTaskId",
                principalTable: "SubTasks",
                principalColumn: "Id");
        }
    }
}
