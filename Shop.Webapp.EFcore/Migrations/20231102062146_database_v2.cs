using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Webapp.EFcore.Migrations
{
    public partial class database_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleTurn",
                table: "shop_product",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "shop_user",
                keyColumn: "Id",
                keyValue: new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"),
                columns: new[] { "CreatedTime", "ResetPasswordExpiry" },
                values: new object[] { new DateTime(2023, 11, 2, 13, 21, 46, 374, DateTimeKind.Local).AddTicks(2193), new DateTime(2023, 11, 2, 6, 21, 46, 374, DateTimeKind.Utc).AddTicks(2188) });

            migrationBuilder.UpdateData(
                table: "shop_userrole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), new Guid("49267eb3-4174-4081-a3e0-c57cfc001353") },
                column: "Id",
                value: new Guid("03357bdc-545a-4392-ab8e-f095c3cebb08"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleTurn",
                table: "shop_product");

            migrationBuilder.UpdateData(
                table: "shop_user",
                keyColumn: "Id",
                keyValue: new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"),
                columns: new[] { "CreatedTime", "ResetPasswordExpiry" },
                values: new object[] { new DateTime(2023, 11, 2, 10, 32, 53, 434, DateTimeKind.Local).AddTicks(2189), new DateTime(2023, 11, 2, 3, 32, 53, 434, DateTimeKind.Utc).AddTicks(2185) });

            migrationBuilder.UpdateData(
                table: "shop_userrole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), new Guid("49267eb3-4174-4081-a3e0-c57cfc001353") },
                column: "Id",
                value: new Guid("c8f09afd-5af0-489d-b747-f1f354d2ef0c"));
        }
    }
}
