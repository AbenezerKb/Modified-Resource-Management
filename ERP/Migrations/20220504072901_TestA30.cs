using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuySiteId",
                table: "Buys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Buys_BuySiteId",
                table: "Buys",
                column: "BuySiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buys_Sites_BuySiteId",
                table: "Buys",
                column: "BuySiteId",
                principalTable: "Sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buys_Sites_BuySiteId",
                table: "Buys");

            migrationBuilder.DropIndex(
                name: "IX_Buys_BuySiteId",
                table: "Buys");

            migrationBuilder.DropColumn(
                name: "BuySiteId",
                table: "Buys");
        }
    }
}
