using Microsoft.EntityFrameworkCore.Migrations;

namespace Battery_Shop.Migrations
{
    public partial class BatteryShop_Required_Typo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BatteryShops_BatteryShopId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BatterShopId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "BatteryShopId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BatteryShops_BatteryShopId",
                table: "Employees",
                column: "BatteryShopId",
                principalTable: "BatteryShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BatteryShops_BatteryShopId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "BatteryShopId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BatterShopId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BatteryShops_BatteryShopId",
                table: "Employees",
                column: "BatteryShopId",
                principalTable: "BatteryShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
