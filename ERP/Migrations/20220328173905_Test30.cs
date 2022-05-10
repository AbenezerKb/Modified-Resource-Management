using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PenalityPercentage",
                table: "AssetDamages",
                type: "decimal(3,3)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PenalityPercentage",
                table: "AssetDamages",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,3)");
        }
    }
}
