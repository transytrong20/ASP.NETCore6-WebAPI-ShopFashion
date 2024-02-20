using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Webapp.EFcore.Migrations
{
    public partial class updatev4 : Migration
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
                newName: "shop_carts");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ProductId",
                table: "shop_carts",
                newName: "IX_shop_carts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shop_carts",
                table: "shop_carts",
                columns: new[] { "UserId", "ProductId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_shop_carts_shop_product_ProductId",
                table: "shop_carts",
                column: "ProductId",
                principalTable: "shop_product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shop_carts_shop_user_UserId",
                table: "shop_carts",
                column: "UserId",
                principalTable: "shop_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shop_carts_shop_product_ProductId",
                table: "shop_carts");

            migrationBuilder.DropForeignKey(
                name: "FK_shop_carts_shop_user_UserId",
                table: "shop_carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shop_carts",
                table: "shop_carts");

            migrationBuilder.RenameTable(
                name: "shop_carts",
                newName: "Carts");

            migrationBuilder.RenameIndex(
                name: "IX_shop_carts_ProductId",
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
                values: new object[] { new DateTime(2024, 1, 14, 18, 13, 58, 161, DateTimeKind.Local).AddTicks(4750), new DateTime(2024, 1, 14, 11, 13, 58, 161, DateTimeKind.Utc).AddTicks(4741) });

            migrationBuilder.UpdateData(
                table: "shop_userrole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), new Guid("49267eb3-4174-4081-a3e0-c57cfc001353") },
                column: "Id",
                value: new Guid("a21100fe-c029-40c0-9bbd-15f0c240d5d6"));

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
