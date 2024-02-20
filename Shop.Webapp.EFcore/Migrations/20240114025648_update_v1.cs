using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Webapp.EFcore.Migrations
{
    public partial class update_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "shop_user",
                keyColumn: "Id",
                keyValue: new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"),
                columns: new[] { "CreatedTime", "ResetPasswordExpiry" },
                values: new object[] { new DateTime(2024, 1, 14, 9, 56, 47, 296, DateTimeKind.Local).AddTicks(3728), new DateTime(2024, 1, 14, 2, 56, 47, 296, DateTimeKind.Utc).AddTicks(3720) });

            migrationBuilder.UpdateData(
                table: "shop_userrole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), new Guid("49267eb3-4174-4081-a3e0-c57cfc001353") },
                column: "Id",
                value: new Guid("0d9416a5-36a8-4465-adae-c3367c0a6a94"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "shop_user",
                keyColumn: "Id",
                keyValue: new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"),
                columns: new[] { "CreatedTime", "ResetPasswordExpiry" },
                values: new object[] { new DateTime(2024, 1, 12, 14, 42, 36, 283, DateTimeKind.Local).AddTicks(3078), new DateTime(2024, 1, 12, 7, 42, 36, 283, DateTimeKind.Utc).AddTicks(3073) });

            migrationBuilder.UpdateData(
                table: "shop_userrole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), new Guid("49267eb3-4174-4081-a3e0-c57cfc001353") },
                column: "Id",
                value: new Guid("0e976542-ffb1-4c21-8d9f-2c8e80d7f3ac"));
        }
    }
}
