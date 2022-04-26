using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Damage",
                table: "BorrowItemEquipmentAssets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "BorrowItemEquipmentAssets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnRemark",
                table: "BorrowItemEquipmentAssets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReturnedById",
                table: "BorrowItemEquipmentAssets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowItemEquipmentAssets_ReturnedById",
                table: "BorrowItemEquipmentAssets",
                column: "ReturnedById");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowItemEquipmentAssets_Employees_ReturnedById",
                table: "BorrowItemEquipmentAssets",
                column: "ReturnedById",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowItemEquipmentAssets_Employees_ReturnedById",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropIndex(
                name: "IX_BorrowItemEquipmentAssets_ReturnedById",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropColumn(
                name: "Damage",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropColumn(
                name: "ReturnRemark",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropColumn(
                name: "ReturnedById",
                table: "BorrowItemEquipmentAssets");
        }
    }
}
