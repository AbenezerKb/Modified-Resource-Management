using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Buys_BuyId",
                table: "PurchaseItems");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Buys_BuyId",
                table: "PurchaseItems",
                column: "BuyId",
                principalTable: "Buys",
                principalColumn: "BuyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Buys_BuyId",
                table: "PurchaseItems");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Buys_BuyId",
                table: "PurchaseItems",
                column: "BuyId",
                principalTable: "Buys",
                principalColumn: "BuyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
