using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class SubcontractorNavPropAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SubContractorId",
                table: "WeeklyPlanValues",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubContractorId",
                table: "PerformanceSheets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyPlanValues_SubContractorId",
                table: "WeeklyPlanValues",
                column: "SubContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceSheets_SubContractorId",
                table: "PerformanceSheets",
                column: "SubContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceSheets_SubContractors_SubContractorId",
                table: "PerformanceSheets",
                column: "SubContractorId",
                principalTable: "SubContractors",
                principalColumn: "SubId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeeklyPlanValues_SubContractors_SubContractorId",
                table: "WeeklyPlanValues",
                column: "SubContractorId",
                principalTable: "SubContractors",
                principalColumn: "SubId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceSheets_SubContractors_SubContractorId",
                table: "PerformanceSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_WeeklyPlanValues_SubContractors_SubContractorId",
                table: "WeeklyPlanValues");

            migrationBuilder.DropIndex(
                name: "IX_WeeklyPlanValues_SubContractorId",
                table: "WeeklyPlanValues");

            migrationBuilder.DropIndex(
                name: "IX_PerformanceSheets_SubContractorId",
                table: "PerformanceSheets");

            migrationBuilder.AlterColumn<int>(
                name: "SubContractorId",
                table: "WeeklyPlanValues",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubContractorId",
                table: "PerformanceSheets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
