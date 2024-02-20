﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Webapp.EFcore;

#nullable disable

namespace Shop.Webapp.EFcore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240114121205_update-v4")]
    partial class updatev4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Shop.Webapp.Domain.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("shop_cart", (string)null);
                });

            modelBuilder.Entity("Shop.Webapp.Domain.CartProduct", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CartId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "ProductId", "CartId");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("shop_cart_product", (string)null);
                });

            modelBuilder.Entity("Shop.Webapp.Domain.Carts", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("shop_carts", (string)null);
                });

            modelBuilder.Entity("Shop.Webapp.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int?>("Index")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("shop_category", (string)null);
                });

            modelBuilder.Entity("Shop.Webapp.Domain.CategoryProduct", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("shop_category_product", (string)null);
                });

            modelBuilder.Entity("Shop.Webapp.Domain.PermissionGrant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProviderName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("shop_permissiongrant", (string)null);
                });

            modelBuilder.Entity("Shop.Webapp.Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool?>("Accepted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<int?>("Index")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("New")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool?>("Sale")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("SaleTurn")
                        .HasColumnType("int");

                    b.Property<int?>("Sold")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("shop_product", (string)null);
                });

            modelBuilder.Entity("Shop.Webapp.Domain.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Static")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("shop_role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"),
                            Description = "Quyền admin, quản lý toàn bộ trang web",
                            IsDefault = false,
                            Name = "manager",
                            Static = true
                        },
                        new
                        {
                            Id = new Guid("9fdf3383-125f-4b87-a8d9-9e235a4e3652"),
                            Description = "tài khoản dành cho user",
                            IsDefault = false,
                            Name = "user",
                            Static = true
                        });
                });

            modelBuilder.Entity("Shop.Webapp.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailCount")
                        .HasColumnType("int");

                    b.Property<bool>("AllowLockUser")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("EmailVerifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("EndLockedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("HashCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("LastModifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("PhoneVerifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ResetPasswordExpiry")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("longtext");

                    b.Property<string>("SurName")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("shop_user", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"),
                            AccessFailCount = 0,
                            AllowLockUser = false,
                            CreatedTime = new DateTime(2024, 1, 14, 19, 12, 4, 662, DateTimeKind.Local).AddTicks(7096),
                            Email = "manager@gmail.com",
                            EmailVerified = true,
                            HashCode = "49267eb3-4174-4081-a3e0-c57cfc001355",
                            IsDeleted = false,
                            IsLocked = false,
                            Name = "Admin Manager",
                            PasswordHash = "FV4IPiVEhApgRQ5/dbS/bMRQbA+0c3Soi5lwlZVLFQ8=",
                            Phone = "1",
                            PhoneVerified = false,
                            ResetPasswordExpiry = new DateTime(2024, 1, 14, 12, 12, 4, 662, DateTimeKind.Utc).AddTicks(7088),
                            SurName = "Tài khoản admin",
                            Username = "manager"
                        });
                });

            modelBuilder.Entity("Shop.Webapp.Domain.UserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("shop_userrole", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("7299f85a-344e-4045-944b-aba6e4cd58a1"),
                            UserId = new Guid("49267eb3-4174-4081-a3e0-c57cfc001353"),
                            Id = new Guid("892330aa-fbe3-44f2-9efb-c175325570a1")
                        });
                });

            modelBuilder.Entity("Shop.Webapp.Domain.CartProduct", b =>
                {
                    b.HasOne("Shop.Webapp.Domain.Cart", "Cart")
                        .WithMany("CartProducts")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Webapp.Domain.Product", "Product")
                        .WithMany("CartProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Webapp.Domain.User", "User")
                        .WithMany("CartProducts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shop.Webapp.Domain.Carts", b =>
                {
                    b.HasOne("Shop.Webapp.Domain.Product", "Product")
                        .WithMany("Cartss")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Webapp.Domain.User", "User")
                        .WithMany("Cartss")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shop.Webapp.Domain.CategoryProduct", b =>
                {
                    b.HasOne("Shop.Webapp.Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Webapp.Domain.Product", "Product")
                        .WithMany("Categories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Webapp.Domain.Product", b =>
                {
                    b.HasOne("Shop.Webapp.Domain.Category", null)
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("Shop.Webapp.Domain.UserRole", b =>
                {
                    b.HasOne("Shop.Webapp.Domain.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Webapp.Domain.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shop.Webapp.Domain.Cart", b =>
                {
                    b.Navigation("CartProducts");
                });

            modelBuilder.Entity("Shop.Webapp.Domain.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Shop.Webapp.Domain.Product", b =>
                {
                    b.Navigation("CartProducts");

                    b.Navigation("Cartss");

                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Shop.Webapp.Domain.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Shop.Webapp.Domain.User", b =>
                {
                    b.Navigation("CartProducts");

                    b.Navigation("Cartss");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
