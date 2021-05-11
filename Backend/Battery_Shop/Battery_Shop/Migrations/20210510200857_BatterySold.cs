using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Battery_Shop.Migrations
{
    public partial class BatterySold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Warrant",
                table: "Batteries",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddColumn<bool>(
                name: "Sold",
                table: "Batteries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sold",
                table: "Batteries");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Warrant",
                table: "Batteries",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
