using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowItemEquipmentAssets_Employees_ReturnedById",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.RenameColumn(
                name: "ReturnedById",
                table: "BorrowItemEquipmentAssets",
                newName: "ReturnId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowItemEquipmentAssets_ReturnedById",
                table: "BorrowItemEquipmentAssets",
                newName: "IX_BorrowItemEquipmentAssets_ReturnId");

            migrationBuilder.CreateTable(
                name: "Return",
                columns: table => new
                {
                    ReturnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnedById = table.Column<int>(type: "int", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Return", x => x.ReturnId);
                    table.ForeignKey(
                        name: "FK_Return_Employees_ReturnedById",
                        column: x => x.ReturnedById,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Return_ReturnedById",
                table: "Return",
                column: "ReturnedById");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowItemEquipmentAssets_Return_ReturnId",
                table: "BorrowItemEquipmentAssets",
                column: "ReturnId",
                principalTable: "Return",
                principalColumn: "ReturnId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowItemEquipmentAssets_Return_ReturnId",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropTable(
                name: "Return");

            migrationBuilder.RenameColumn(
                name: "ReturnId",
                table: "BorrowItemEquipmentAssets",
                newName: "ReturnedById");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowItemEquipmentAssets_ReturnId",
                table: "BorrowItemEquipmentAssets",
                newName: "IX_BorrowItemEquipmentAssets_ReturnedById");

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "BorrowItemEquipmentAssets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "BorrowItemEquipmentAssets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowItemEquipmentAssets_Employees_ReturnedById",
                table: "BorrowItemEquipmentAssets",
                column: "ReturnedById",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }
    }
}
