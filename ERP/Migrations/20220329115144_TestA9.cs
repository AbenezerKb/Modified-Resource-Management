using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveredFrom",
                table: "Receives");

            migrationBuilder.DropColumn(
                name: "ApproveRemark",
                table: "ReceiveItems");

            migrationBuilder.AddColumn<string>(
                name: "ApproveRemark",
                table: "Receives",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproveRemark",
                table: "Receives");

            migrationBuilder.AddColumn<string>(
                name: "DeliveredFrom",
                table: "Receives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApproveRemark",
                table: "ReceiveItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
