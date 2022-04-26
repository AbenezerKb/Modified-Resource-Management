using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemSiteQties_Stores_SiteId",
                table: "ItemSiteQties");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSiteQties_Sites_SiteId",
                table: "ItemSiteQties",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemSiteQties_Sites_SiteId",
                table: "ItemSiteQties");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSiteQties_Stores_SiteId",
                table: "ItemSiteQties",
                column: "SiteId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
