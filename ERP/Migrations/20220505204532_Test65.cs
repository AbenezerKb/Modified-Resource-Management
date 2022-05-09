using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test65 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Employees_DeliveredById",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_DeliveredById",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "DeliveredById",
                table: "Transfers");

            migrationBuilder.AddColumn<string>(
                name: "DeliveredBy",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveredBy",
                table: "Transfers");

            migrationBuilder.AddColumn<int>(
                name: "DeliveredById",
                table: "Transfers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_DeliveredById",
                table: "Transfers",
                column: "DeliveredById");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Employees_DeliveredById",
                table: "Transfers",
                column: "DeliveredById",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
