using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SiteId",
                table: "Notifications",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Sites_SiteId",
                table: "Notifications",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Sites_SiteId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SiteId",
                table: "Notifications");
        }
    }
}
