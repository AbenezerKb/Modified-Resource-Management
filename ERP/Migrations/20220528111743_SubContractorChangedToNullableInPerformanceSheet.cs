using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class SubContractorChangedToNullableInPerformanceSheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceSheets_SubContractors_SubContractorId",
                table: "PerformanceSheets");

            migrationBuilder.AlterColumn<int>(
                name: "SubContractorId",
                table: "PerformanceSheets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceSheets_SubContractors_SubContractorId",
                table: "PerformanceSheets",
                column: "SubContractorId",
                principalTable: "SubContractors",
                principalColumn: "SubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceSheets_SubContractors_SubContractorId",
                table: "PerformanceSheets");

            migrationBuilder.AlterColumn<int>(
                name: "SubContractorId",
                table: "PerformanceSheets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceSheets_SubContractors_SubContractorId",
                table: "PerformanceSheets",
                column: "SubContractorId",
                principalTable: "SubContractors",
                principalColumn: "SubId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
