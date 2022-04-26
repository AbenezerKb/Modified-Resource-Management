using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "Returns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Returns_SiteId",
                table: "Returns",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowItemEquipmentAssets_Borrows_BorrowId",
                table: "BorrowItemEquipmentAssets",
                column: "BorrowId",
                principalTable: "Borrows",
                principalColumn: "BorrowId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_Sites_SiteId",
                table: "Returns",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowItemEquipmentAssets_Borrows_BorrowId",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_Returns_Sites_SiteId",
                table: "Returns");

            migrationBuilder.DropIndex(
                name: "IX_Returns_SiteId",
                table: "Returns");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "Returns");
        }
    }
}
