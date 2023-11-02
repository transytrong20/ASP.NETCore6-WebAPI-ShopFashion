using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Webapp.EFcore.Migrations
{
    public partial class database_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "New",
                table: "shop_product",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Sale",
                table: "shop_product",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "shop_role",
                columns: new[] { "Id", "Description", "IsDefault", "Name", "Static" },
                values: new object[] { new Guid("9fdf3383-125f-4b87-a8d9-9e235a4e3652"), "tài khoản dành cho user", false, "user", true });

            migrationBuilder.UpdateData(
                table: "shop_user",
                keyColumn: "Id",
                keyValue: new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"),
                columns: new[] { "CreatedTime", "Phone", "ResetPasswordExpiry" },
                values: new object[] { new DateTime(2023, 11, 2, 10, 32, 53, 434, DateTimeKind.Local).AddTicks(2189), "1", new DateTime(2023, 11, 2, 3, 32, 53, 434, DateTimeKind.Utc).AddTicks(2185) });

            migrationBuilder.UpdateData(
                table: "shop_userrole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), new Guid("49267eb3-4174-4081-a3e0-c57cfc001353") },
                column: "Id",
                value: new Guid("c8f09afd-5af0-489d-b747-f1f354d2ef0c"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "shop_role",
                keyColumn: "Id",
                keyValue: new Guid("9fdf3383-125f-4b87-a8d9-9e235a4e3652"));

            migrationBuilder.DropColumn(
                name: "New",
                table: "shop_product");

            migrationBuilder.DropColumn(
                name: "Sale",
                table: "shop_product");

            migrationBuilder.UpdateData(
                table: "shop_user",
                keyColumn: "Id",
                keyValue: new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"),
                columns: new[] { "CreatedTime", "Phone", "ResetPasswordExpiry" },
                values: new object[] { new DateTime(2023, 10, 31, 10, 28, 43, 94, DateTimeKind.Local).AddTicks(5928), null, new DateTime(2023, 10, 31, 3, 28, 43, 94, DateTimeKind.Utc).AddTicks(5923) });

            migrationBuilder.UpdateData(
                table: "shop_userrole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), new Guid("49267eb3-4174-4081-a3e0-c57cfc001353") },
                column: "Id",
                value: new Guid("5423e218-df68-4bdf-a3db-0c2608a18d1d"));
        }
    }
}
