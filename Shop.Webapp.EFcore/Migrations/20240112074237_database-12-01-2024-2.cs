using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Webapp.EFcore.Migrations
{
    public partial class database120120242 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_shop_product_ProductId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_shop_user_UserId",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "shop_cart");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ProductId",
                table: "shop_cart",
                newName: "IX_shop_cart_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shop_cart",
                table: "shop_cart",
                columns: new[] { "UserId", "ProductId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_shop_cart_shop_product_ProductId",
                table: "shop_cart",
                column: "ProductId",
                principalTable: "shop_product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shop_cart_shop_user_UserId",
                table: "shop_cart",
                column: "UserId",
                principalTable: "shop_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shop_cart_shop_product_ProductId",
                table: "shop_cart");

            migrationBuilder.DropForeignKey(
                name: "FK_shop_cart_shop_user_UserId",
                table: "shop_cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shop_cart",
                table: "shop_cart");

            migrationBuilder.RenameTable(
                name: "shop_cart",
                newName: "Carts");

            migrationBuilder.RenameIndex(
                name: "IX_shop_cart_ProductId",
                table: "Carts",
                newName: "IX_Carts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "shop_user",
                keyColumn: "Id",
                keyValue: new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"),
                columns: new[] { "CreatedTime", "ResetPasswordExpiry" },
                values: new object[] { new DateTime(2024, 1, 12, 14, 40, 36, 80, DateTimeKind.Local).AddTicks(8141), new DateTime(2024, 1, 12, 7, 40, 36, 80, DateTimeKind.Utc).AddTicks(8135) });

            migrationBuilder.UpdateData(
                table: "shop_userrole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), new Guid("49267eb3-4174-4081-a3e0-c57cfc001353") },
                column: "Id",
                value: new Guid("f11c256d-e63c-49b3-90ab-04b3e0a5a8a5"));

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_shop_product_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "shop_product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_shop_user_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "shop_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
