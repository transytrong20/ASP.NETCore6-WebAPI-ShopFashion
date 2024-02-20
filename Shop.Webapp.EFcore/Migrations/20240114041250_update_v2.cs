using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Webapp.EFcore.Migrations
{
    public partial class update_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_shop_cart_ProductId",
                table: "shop_cart");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "shop_cart");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "shop_cart");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shop_cart",
                table: "shop_cart",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "shop_cart_product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CartId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop_cart_product", x => new { x.UserId, x.ProductId, x.CartId });
                    table.ForeignKey(
                        name: "FK_shop_cart_product_shop_cart_CartId",
                        column: x => x.CartId,
                        principalTable: "shop_cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shop_cart_product_shop_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "shop_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shop_cart_product_shop_user_UserId",
                        column: x => x.UserId,
                        principalTable: "shop_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "shop_user",
                keyColumn: "Id",
                keyValue: new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"),
                columns: new[] { "CreatedTime", "ResetPasswordExpiry" },
                values: new object[] { new DateTime(2024, 1, 14, 11, 12, 50, 35, DateTimeKind.Local).AddTicks(9214), new DateTime(2024, 1, 14, 4, 12, 50, 35, DateTimeKind.Utc).AddTicks(9204) });

            migrationBuilder.UpdateData(
                table: "shop_userrole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), new Guid("49267eb3-4174-4081-a3e0-c57cfc001353") },
                column: "Id",
                value: new Guid("66cd2269-db59-4521-8c52-16d936500b3c"));

            migrationBuilder.CreateIndex(
                name: "IX_shop_cart_product_CartId",
                table: "shop_cart_product",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_shop_cart_product_ProductId",
                table: "shop_cart_product",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shop_cart_product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shop_cart",
                table: "shop_cart");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "shop_cart",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "shop_cart",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shop_cart",
                table: "shop_cart",
                columns: new[] { "UserId", "ProductId" });

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

            migrationBuilder.CreateIndex(
                name: "IX_shop_cart_ProductId",
                table: "shop_cart",
                column: "ProductId");

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
    }
}
