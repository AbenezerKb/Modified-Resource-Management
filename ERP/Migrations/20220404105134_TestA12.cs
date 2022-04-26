using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceivingSiteId",
                table: "Receives",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receives_ReceivingSiteId",
                table: "Receives",
                column: "ReceivingSiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receives_Sites_ReceivingSiteId",
                table: "Receives",
                column: "ReceivingSiteId",
                principalTable: "Sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receives_Sites_ReceivingSiteId",
                table: "Receives");

            migrationBuilder.DropIndex(
                name: "IX_Receives_ReceivingSiteId",
                table: "Receives");

            migrationBuilder.DropColumn(
                name: "ReceivingSiteId",
                table: "Receives");
        }
    }
}
