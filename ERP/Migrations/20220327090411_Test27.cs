using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowItemEquipmentAssets_Return_ReturnId",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_Return_Employees_ReturnedById",
                table: "Return");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Return",
                table: "Return");

            migrationBuilder.RenameTable(
                name: "Return",
                newName: "Returns");

            migrationBuilder.RenameIndex(
                name: "IX_Return_ReturnedById",
                table: "Returns",
                newName: "IX_Returns_ReturnedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Returns",
                table: "Returns",
                column: "ReturnId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowItemEquipmentAssets_Returns_ReturnId",
                table: "BorrowItemEquipmentAssets",
                column: "ReturnId",
                principalTable: "Returns",
                principalColumn: "ReturnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_Employees_ReturnedById",
                table: "Returns",
                column: "ReturnedById",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowItemEquipmentAssets_Returns_ReturnId",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_Returns_Employees_ReturnedById",
                table: "Returns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Returns",
                table: "Returns");

            migrationBuilder.RenameTable(
                name: "Returns",
                newName: "Return");

            migrationBuilder.RenameIndex(
                name: "IX_Returns_ReturnedById",
                table: "Return",
                newName: "IX_Return_ReturnedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Return",
                table: "Return",
                column: "ReturnId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowItemEquipmentAssets_Return_ReturnId",
                table: "BorrowItemEquipmentAssets",
                column: "ReturnId",
                principalTable: "Return",
                principalColumn: "ReturnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Return_Employees_ReturnedById",
                table: "Return",
                column: "ReturnedById",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
