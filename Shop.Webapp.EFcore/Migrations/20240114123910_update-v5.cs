using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Webapp.EFcore.Migrations
{
    public partial class updatev5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "shop_product",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "shop_user",
                keyColumn: "Id",
                keyValue: new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"),
                columns: new[] { "CreatedTime", "ResetPasswordExpiry" },
                values: new object[] { new DateTime(2024, 1, 14, 19, 39, 9, 321, DateTimeKind.Local).AddTicks(1946), new DateTime(2024, 1, 14, 12, 39, 9, 321, DateTimeKind.Utc).AddTicks(1940) });

            migrationBuilder.UpdateData(
                table: "shop_userrole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), new Guid("49267eb3-4174-4081-a3e0-c57cfc001353") },
                column: "Id",
                value: new Guid("2fa86abc-fc2f-4199-b563-786946d6444b"));

            migrationBuilder.CreateIndex(
                name: "IX_shop_product_UserId",
                table: "shop_product",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_shop_product_shop_user_UserId",
                table: "shop_product",
                column: "UserId",
                principalTable: "shop_user",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shop_product_shop_user_UserId",
                table: "shop_product");

            migrationBuilder.DropIndex(
                name: "IX_shop_product_UserId",
                table: "shop_product");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "shop_product");

            migrationBuilder.UpdateData(
                table: "shop_user",
                keyColumn: "Id",
                keyValue: new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"),
                columns: new[] { "CreatedTime", "ResetPasswordExpiry" },
                values: new object[] { new DateTime(2024, 1, 14, 19, 12, 4, 662, DateTimeKind.Local).AddTicks(7096), new DateTime(2024, 1, 14, 12, 12, 4, 662, DateTimeKind.Utc).AddTicks(7088) });

            migrationBuilder.UpdateData(
                table: "shop_userrole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), new Guid("49267eb3-4174-4081-a3e0-c57cfc001353") },
                column: "Id",
                value: new Guid("892330aa-fbe3-44f2-9efb-c175325570a1"));
        }
    }
}
