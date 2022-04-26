using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Buys_BuyId",
                table: "PurchaseItems");

            migrationBuilder.RenameColumn(
                name: "BuyId",
                table: "PurchaseItems",
                newName: "RequestedById");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseItems_BuyId",
                table: "PurchaseItems",
                newName: "IX_PurchaseItems_RequestedById");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Employees_RequestedById",
                table: "PurchaseItems",
                column: "RequestedById",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Employees_RequestedById",
                table: "PurchaseItems");

            migrationBuilder.RenameColumn(
                name: "RequestedById",
                table: "PurchaseItems",
                newName: "BuyId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseItems_RequestedById",
                table: "PurchaseItems",
                newName: "IX_PurchaseItems_BuyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Buys_BuyId",
                table: "PurchaseItems",
                column: "BuyId",
                principalTable: "Buys",
                principalColumn: "BuyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
