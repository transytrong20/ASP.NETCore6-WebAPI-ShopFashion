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
    [Migration("20231025041500_update-user")]
    partial class updateuser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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
                            CreatedTime = new DateTime(2023, 10, 25, 11, 15, 0, 361, DateTimeKind.Local).AddTicks(1670),
                            Email = "manager@gmail.com",
                            EmailVerified = true,
                            HashCode = "49267eb3-4174-4081-a3e0-c57cfc001355",
                            IsDeleted = false,
                            IsLocked = false,
                            Name = "Admin Manager",
                            PasswordHash = "FV4IPiVEhApgRQ5/dbS/bMRQbA+0c3Soi5lwlZVLFQ8=",
                            PhoneVerified = false,
                            ResetPasswordExpiry = new DateTime(2023, 10, 25, 4, 15, 0, 361, DateTimeKind.Utc).AddTicks(1666),
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
                            Id = new Guid("b0308b26-9ac8-4dc0-846b-18f8b0aa1cfe")
                        });
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

            modelBuilder.Entity("Shop.Webapp.Domain.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Shop.Webapp.Domain.User", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
