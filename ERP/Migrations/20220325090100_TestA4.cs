using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_Employees_EmployeeId",
                table: "UserAccounts");

            migrationBuilder.RenameColumn(
                name: "CanTransfer",
                table: "UserRoles",
                newName: "CanTransferMaterial");

            migrationBuilder.RenameColumn(
                name: "CanFixedAsset",
                table: "UserRoles",
                newName: "CanTransferEquipment");

            migrationBuilder.AddColumn<int>(
                name: "CanApproveIssue",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanApproveMaintainance",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanApprovePurchase",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanApproveTransfer",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanCheckPurchase",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanCheckRecieve",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "UserAccounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_Employees_EmployeeId",
                table: "UserAccounts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_Employees_EmployeeId",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "CanApproveIssue",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanApproveMaintainance",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanApprovePurchase",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanApproveTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanCheckPurchase",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanCheckRecieve",
                table: "UserRoles");

            migrationBuilder.RenameColumn(
                name: "CanTransferMaterial",
                table: "UserRoles",
                newName: "CanTransfer");

            migrationBuilder.RenameColumn(
                name: "CanTransferEquipment",
                table: "UserRoles",
                newName: "CanFixedAsset");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "UserAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_Employees_EmployeeId",
                table: "UserAccounts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
