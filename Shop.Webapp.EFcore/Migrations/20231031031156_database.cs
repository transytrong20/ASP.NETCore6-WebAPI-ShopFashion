using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Webapp.EFcore.Migrations
{
    public partial class database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shop_category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Index = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop_category", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shop_permissiongrant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PermissionName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop_permissiongrant", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shop_role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Static = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop_role", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shop_user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SurName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HashCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EmailVerifiedTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PhoneVerifiedTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Avatar = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AllowLockUser = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsLocked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailCount = table.Column<int>(type: "int", nullable: false),
                    EndLockedTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ResetPasswordToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResetPasswordExpiry = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop_user", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shop_product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Sold = table.Column<int>(type: "int", nullable: true),
                    Discount = table.Column<int>(type: "int", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    Accepted = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shop_product_shop_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "shop_category",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shop_userrole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop_userrole", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_shop_userrole_shop_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "shop_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shop_userrole_shop_user_UserId",
                        column: x => x.UserId,
                        principalTable: "shop_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shop_category_product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop_category_product", x => new { x.CategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_shop_category_product_shop_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "shop_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shop_category_product_shop_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "shop_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "shop_role",
                columns: new[] { "Id", "Description", "IsDefault", "Name", "Static" },
                values: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), "Quyền admin, quản lý toàn bộ trang web", false, "manager", true });

            migrationBuilder.InsertData(
                table: "shop_user",
                columns: new[] { "Id", "AccessFailCount", "AllowLockUser", "Avatar", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Email", "EmailVerified", "EmailVerifiedTime", "EndLockedTime", "HashCode", "IsDeleted", "IsLocked", "LastModifiedBy", "LastModifiedTime", "Name", "PasswordHash", "Phone", "PhoneVerified", "PhoneVerifiedTime", "ResetPasswordExpiry", "ResetPasswordToken", "SurName", "Username" },
                values: new object[] { new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"), 0, false, null, null, new DateTime(2023, 10, 31, 10, 11, 56, 240, DateTimeKind.Local).AddTicks(7913), null, null, "manager@gmail.com", true, null, null, "49267eb3-4174-4081-a3e0-c57cfc001355", false, false, null, null, "Admin Manager", "FV4IPiVEhApgRQ5/dbS/bMRQbA+0c3Soi5lwlZVLFQ8=", null, false, null, new DateTime(2023, 10, 31, 3, 11, 56, 240, DateTimeKind.Utc).AddTicks(7909), null, "Tài khoản admin", "manager" });

            migrationBuilder.InsertData(
                table: "shop_userrole",
                columns: new[] { "RoleId", "UserId", "Id" },
                values: new object[] { new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"), new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"), new Guid("3b213aa4-2348-4d63-98cc-a56f68fd7ad7") });

            migrationBuilder.CreateIndex(
                name: "IX_shop_category_product_ProductId",
                table: "shop_category_product",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_shop_product_CategoryId",
                table: "shop_product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_shop_userrole_UserId",
                table: "shop_userrole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shop_category_product");

            migrationBuilder.DropTable(
                name: "shop_permissiongrant");

            migrationBuilder.DropTable(
                name: "shop_userrole");

            migrationBuilder.DropTable(
                name: "shop_product");

            migrationBuilder.DropTable(
                name: "shop_role");

            migrationBuilder.DropTable(
                name: "shop_user");

            migrationBuilder.DropTable(
                name: "shop_category");
        }
    }
}
